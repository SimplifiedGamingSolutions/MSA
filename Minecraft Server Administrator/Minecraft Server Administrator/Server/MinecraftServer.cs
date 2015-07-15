using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Technewlogic.WpfDialogManagement;
using Technewlogic.WpfDialogManagement.Contracts;

namespace Minecraft_Server_Administrator.Server
{
    public class MinecraftServer
    {
        public static MinecraftServer current;
        public ServerConfiguration config;
        public ServerProperties props;

        public MinecraftServer()
        {
            current = this;
            loadConfig(this);
            if(config.type == ServerConfiguration.ServerType.Forge)
            {
                if(config.serverFile.Equals(""))
                {
                    IMessageDialog install = MainWindowContent.dialogManager.CreateMessageDialog("No server loaded, would you like to create a new one or load?", DialogMode.YesNo);
                    install.YesText = "Create";
                    install.NoText = "Load";
                    install.Yes = () => { installForge(this); };
                    install.No = () => { OpenFileDialog dialog = new OpenFileDialog(); if (dialog.ShowDialog() == true) { loadConfig(MinecraftServer.current, dialog.FileName); } };
                    install.Show();
                }
                props = new ServerProperties(Path.Combine(config.directory, "server.properties"));
            }
        }


        internal static void loadConfig(MinecraftServer server)
        {
            if (File.Exists(@"data.msa"))
            {
                server.config = ServerConfiguration.deserializeFromXML(@"data.msa");
            }
            else
            {
                server.config = new ServerConfiguration();
            }

        }
        internal static void loadConfig(MinecraftServer server, string path)
        {
            if (File.Exists(path))
            {
                server.config = ServerConfiguration.deserializeFromXML(path);
            }
            else
            {
                server.config = new ServerConfiguration();
            }
        }
        private static WebClient client;
        private static IProgressDialog downloadForgeDialog;
        private static IWaitDialog installForgeDialog;
        private void installForge(MinecraftServer server)
        {

            if (!Directory.Exists(server.config.directory))
            {
                try
                {
                    Directory.CreateDirectory(server.config.directory);
                }
                catch
                {
                    return;
                }
            }
                client = new WebClient();
                downloadForgeDialog = MainWindowContent.dialogManager.CreateProgressDialog("Downloading forge installer", "Download complete", DialogMode.OkCancel);
                installForgeDialog = MainWindowContent.dialogManager.CreateWaitDialog("Installing Forge Server", "Forge Installed", DialogMode.OkCancel);

                initializeInstallForgeDialog();

                initializeDownloadForgeDialog();

        }

        private void initializeInstallForgeDialog()
        {
            installForgeDialog.Cancel = () => { client.CancelAsync(); };
            installForgeDialog.CanOk = false;
            installForgeDialog.Ok = () =>
            {

                File.Delete(Path.Combine(MinecraftServer.current.config.directory, "ForgeInstaller.jar"));
                MinecraftServer.current.config.serverFile = new DirectoryInfo("Server").GetFiles(@"forge-*.jar")[0].Name;

                StreamWriter eula = File.CreateText(Path.Combine(MinecraftServer.current.config.directory, "eula.txt"));
                eula.WriteLine("eula=true");
                eula.Flush();
                eula.Close();
                MinecraftServer.current.config.serializeToXML(@"data.msa");
            };
        }

        private void initializeDownloadForgeDialog()
        {
            String javaDirectory = Environment.GetEnvironmentVariable("JAVA_HOME");
            String javapath;
            if (javaDirectory == null)
            {
                OpenFileDialog dialog = new OpenFileDialog(); if (dialog.ShowDialog() == true) { javapath = dialog.FileName; Environment.SetEnvironmentVariable("JAVA_HOME", javapath.Substring(0, javapath.LastIndexOf('\\')).Substring(0, javapath.LastIndexOf('\\'))); } else { return; }
            }
            else
            {
                javapath = Path.Combine(javaDirectory, "bin", "java.exe");
            }
            downloadForgeDialog.Cancel = () => { client.CancelAsync(); return; };
            downloadForgeDialog.CanOk = false;
            downloadForgeDialog.Ok = () =>
            {

                installForgeDialog.Show(() =>
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = javapath,
                            Arguments = "-jar ForgeInstaller.jar --installServer",
                            WorkingDirectory = Path.Combine(MinecraftServer.current.config.directory)
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    installForgeDialog.CanOk = true;
                });
            };

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://files.minecraftforge.net/maven/net/minecraftforge/forge/");
            string url = doc.DocumentNode.Descendants("a")
                          .Select(a => a.GetAttributeValue("href", null))
                          .Where(u => !String.IsNullOrEmpty(u))
                          .Where(u => u.Contains("installer.jar"))
                          .Where(u => !u.Contains("adfoc.us"))
                          .First();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(
                (sender, e) =>
                {
                    double bytesIn = double.Parse(e.BytesReceived.ToString());
                    double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentage = bytesIn / totalBytes * 100;
                    downloadForgeDialog.Progress = int.Parse(Math.Truncate(percentage).ToString());
                });
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(
                (sender, e) =>
                {
                    downloadForgeDialog.CanOk = true;
                });
            client.DownloadFileAsync(new Uri(url), Path.Combine(MinecraftServer.current.config.directory, "ForgeInstaller.jar"));
            downloadForgeDialog.Show();
        }

        public void startServer()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(config.directory);

                String javaDirectory = Environment.GetEnvironmentVariable("JAVA_HOME");
                String javapath;
                if (javaDirectory == null)
                {
                    OpenFileDialog dialog = new OpenFileDialog(); if (dialog.ShowDialog() == true) { javapath = dialog.FileName; Environment.SetEnvironmentVariable("JAVA_HOME", javapath.Substring(0, javapath.LastIndexOf('\\')).Substring(0, javapath.LastIndexOf('\\'))); } else { return; }
                }
                else
                {
                    javapath = Path.Combine(javaDirectory, "bin", "java.exe");
                }

                MainWindowContent.instance.Console.StartProcess(javapath, "-jar " + config.serverFile + " nogui");
                Directory.SetCurrentDirectory(currentDirectory);
                MainWindowContent.instance.buttonStart.IsEnabled = false;
                MainWindowContent.instance.buttonStop.IsEnabled = true;
                MainWindowContent.instance.buttonRestart.IsEnabled = true;
            }
            catch
            {

            }
        }
        public void stopServer()
        {
            MainWindowContent.instance.Console.WriteInput("stop", Colors.White, false);
            MainWindowContent.instance.buttonStop.IsEnabled = false;
            MainWindowContent.instance.buttonStart.IsEnabled = true;
            MainWindowContent.instance.buttonRestart.IsEnabled = false;
        }
        public bool isRunning()
        {
            return MainWindowContent.instance.Console.IsProcessRunning;
        }

        public void loadProperties()
        {
            props.loadProperties(Path.Combine(config.directory, "server.properties"));
        }


        internal void sendCommand(string command)
        {
            MainWindowContent.instance.Console.WriteInput(command, Colors.White, false);
        }
    }
}
