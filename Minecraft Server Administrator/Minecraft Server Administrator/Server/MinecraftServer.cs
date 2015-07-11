using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
        DialogManager dialogManager = new DialogManager(MainWindowContent.instance, MainWindowContent.instance.Dispatcher);
        public ServerConfiguration config;
        public static MinecraftServer instance;

        public MinecraftServer(ServerConfiguration config)
        {
            instance = this;
            this.config = config;
            if(config.type == ServerConfiguration.ServerType.Forge)
            {
                if(config.serverFile.Equals(""))
                {
                    IMessageDialog install = dialogManager.CreateMessageDialog("There is no server currently installed, would you like to install forge?", DialogMode.YesNo);
                    install.Yes = () => { installForge(config.serverDirectory); };
                    install.No = () => { MessageBox.Show("Server not started."); };
                    install.Show();
                    return;
                }
                else
                {
                    startServer();
                }
            }
        }

        private void installForge(string serverDirectory)
        {

            if (!Directory.Exists(serverDirectory))
            {
                try
                {
                    Directory.CreateDirectory(serverDirectory);
                }
                catch
                {
                    return;
                }
            }
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://files.minecraftforge.net/maven/net/minecraftforge/forge/");
            string url = doc.DocumentNode.Descendants("a")
                          .Select(a => a.GetAttributeValue("href", null))
                          .Where(u => !String.IsNullOrEmpty(u))
                          .Where(u => u.Contains("installer.jar"))
                          .Where(u => !u.Contains("adfoc.us"))
                          .First();
            using (WebClient client = new WebClient())
            {

                IProgressDialog downloadForgeDialog = dialogManager.CreateProgressDialog("Downloading forge installer", "Download complete", DialogMode.OkCancel);
                IWaitDialog installForgeDialog = dialogManager.CreateWaitDialog("Installing Forge Server", "Forge Installed", DialogMode.OkCancel);
                
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
                                FileName = Path.Combine(Environment.GetEnvironmentVariable("JAVA_HOME"), "bin","java.exe"),
                                Arguments = "-jar ForgeInstaller.jar --installServer",
                                WorkingDirectory = "Server"
                            }
                        };
                        process.Start();
                        process.WaitForExit();
                        installForgeDialog.CanOk = true;
                    });
                };

                installForgeDialog.Cancel = () => { client.CancelAsync(); };
                installForgeDialog.CanOk = false;
                installForgeDialog.Ok = () =>
                {

                    File.Delete(@"Server\ForgeInstaller.jar");
                    config.serverFile = new DirectoryInfo("Server").GetFiles(@"forge-*.jar")[0].Name;

                    StreamWriter eula = File.CreateText(@"Server\eula.txt");
                    eula.WriteLine("eula=true");
                    eula.Flush();
                    eula.Close();
                    config.serializeToXML(@"Server\data.msa");
                    startServer();
                };

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
                client.DownloadFileAsync(new Uri(url), Path.Combine(serverDirectory, "ForgeInstaller.jar"));
                downloadForgeDialog.Show();
            }
        }

        public void startServer()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(config.serverDirectory);
                MainWindowContent.instance.Console.StartProcess(Path.Combine(Environment.GetEnvironmentVariable("JAVA_HOME"), "bin", "java.exe"), "-jar " + config.serverFile + " nogui");
                Directory.SetCurrentDirectory(currentDirectory);
                MainWindowContent.instance.buttonStart.IsEnabled = false;
                MainWindowContent.instance.buttonStop.IsEnabled = true;
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
        }
        public bool isRunning()
        {
            return MainWindowContent.instance.Console.IsProcessRunning;
        }
    }
}
