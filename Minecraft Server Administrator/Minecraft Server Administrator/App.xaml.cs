using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Minecraft_Server_Administrator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Minecraft_Server_Administrator.MainWindowContent.instance.Console.WriteInput("stop", Colors.White, false);
            Thread.Sleep(1000);
            if(Minecraft_Server_Administrator.MainWindowContent.instance.Console.IsProcessRunning)
            {
                Minecraft_Server_Administrator.MainWindowContent.instance.Console.StopProcess();
            }
        }
    }
}
