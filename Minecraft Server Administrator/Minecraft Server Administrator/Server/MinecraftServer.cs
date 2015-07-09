using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Technewlogic.WpfDialogManagement;
using Technewlogic.WpfDialogManagement.Contracts;

namespace Minecraft_Server_Administrator.Server
{
    public class MinecraftServer
    {
        public ServerConfiguration config;
        public MinecraftServer(ServerConfiguration config)
        {
            this.config = config;
            if(config.type == ServerConfiguration.ServerType.Forge)
            {
                if(!Directory.Exists(config.serverDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(config.serverDirectory);
                    }
                    catch
                    {
                        return;
                    }
                }
                if(!File.Exists(Path.Combine(config.serverDirectory, "Forge.jar")))
                {

                    HtmlWeb hw = new HtmlWeb();
                    HtmlDocument doc = hw.Load("http://files.minecraftforge.net/maven/net/minecraftforge/forge/");
                    string link = doc.DocumentNode.Descendants("a")
                                  .Select(a => a.GetAttributeValue("href", null))
                                  .Where(u => !String.IsNullOrEmpty(u))
                                  .Where(u => u.Contains("installer.jar"))
                                  .Where(u => !u.Contains("adfoc.us"))
                                  .First();
                    startDownload(link, config.serverDirectory, "ForgeInstaller.jar");
                }
            }
        }
        IProgressDialog progressDialog;
        IWaitDialog waitDialog;
        private void startDownload(string url, string path, string filename)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(file_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(file_DownloadComplete);
                client.DownloadFileAsync(new Uri(url), Path.Combine(path, "ForgeInstaller.jar"));
                DialogManager dialogManager = new DialogManager(MainWindowContent.instance, MainWindowContent.instance.Dispatcher);
                progressDialog = dialogManager.CreateProgressDialog("Downloading forge installer", "Download complete", DialogMode.None);
                progressDialog.CloseWhenWorkerFinished = true;
                progressDialog.Show(() => { while (client.IsBusy) { } });
            }
            Task.Run(() =>
            {
                while (progressDialog.Progress != 100)
                {
                    Directory.SetCurrentDirectory("Server");
                    MainWindowContent.instance.Console.StartProcess(@"C:\Program Files\Java\jdk1.7.0_79\bin\java.exe", "-jar " + "ForgeInstaller.jar --installServer");
                    while (MainWindowContent.instance.Console.IsProcessRunning) { }
                    File.Delete("ForgeInstaller.jar");
                    config.serverFile = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles(@"forge-*.jar")[0].FullName;
                    MainWindowContent.instance.Console.StartProcess(@"C:\Program Files\Java\jdk1.7.0_79\bin\java.exe", "-jar " + config.serverFile);
                }
            });
        }
        void file_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            progressDialog.Progress = int.Parse(Math.Truncate(percentage).ToString());
        }
        void file_DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            progressDialog.Progress = 100;
            progressDialog.Message = "Complete";
            progressDialog.Close();
        }
    }
}
