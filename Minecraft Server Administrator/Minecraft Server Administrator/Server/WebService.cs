using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Minecraft_Server_Administrator.Server
{
    public class WebService
    {
        public WebService()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_EventHandler);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerAsync();
        }

        private void bw_EventHandler(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker b = sender as BackgroundWorker;
            RunServer(b);
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] report = (string[]) e.UserState;
            if (report[0].Equals("add"))
                MainWindowContent.instance.addPlayer(report[1]);
            else if (report[0].Equals("remove"))
                MainWindowContent.instance.removePlayer(report[1]);
        }
        public void RunServer(BackgroundWorker bw)
        {
            var prefix = "http://localhost:39640/";
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            try
            {
                listener.Start();
            }
            catch (HttpListenerException hlex)
            {
                return;
            }
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                ProcessRequest(bw, context);
            }
            listener.Close();
        }

        private void ProcessRequest(BackgroundWorker bw, HttpListenerContext context)
        {
            string request = context.Request.RawUrl;
            if(request.Equals("/PlayerJoined"))
            {
                ProcessPlayerJoined(bw, context);
            }
            else if(request.Equals("/PlayerLeft"))
            {
                ProcessPlayerLeft(bw, context);
            }
        }
        private void ProcessPlayerLeft(BackgroundWorker bw, HttpListenerContext context)
        {
            string name = HttpUtility.UrlDecode(new StreamReader(context.Request.InputStream).ReadLine(), System.Text.Encoding.UTF8);
            bw.ReportProgress(0, new string[] { "remove", name });
            string response = HttpUtility.UrlEncode("Name Created: " + name);
            context.Response.StatusCode = 200;
            context.Response.KeepAlive = false;
            //context.Response.ContentLength64 = b.Length;
            var output = new StreamWriter(context.Response.OutputStream);
            output.WriteLine(response);
            output.Flush();
            context.Response.Close();
        }
        private void ProcessPlayerJoined(BackgroundWorker bw, HttpListenerContext context)
        {
            string name = HttpUtility.UrlDecode(new StreamReader(context.Request.InputStream).ReadLine(), System.Text.Encoding.UTF8);
            bw.ReportProgress(0, new string[]{"add", name});
            string response = HttpUtility.UrlEncode("Name Created: " + name);
            context.Response.StatusCode = 200;
            context.Response.KeepAlive = false;
            //context.Response.ContentLength64 = response.Length;
            var output = new StreamWriter(context.Response.OutputStream);
            output.WriteLine(response);
            output.Flush();
            context.Response.Close();
        }
    }
}
