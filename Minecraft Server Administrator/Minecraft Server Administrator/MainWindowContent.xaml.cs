using Minecraft_Server_Administrator.Server;
using Minecraft_Server_Administrator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minecraft_Server_Administrator
{
    /// <summary>
    /// Interaction logic for MainWindowContent.xaml
    /// </summary>
    public partial class MainWindowContent : UserControl
    {
        public static MainWindowContent instance;
        private readonly MainViewModel viewModel;
        private PlayerCommandMenu pc;
        private SortedSet<string> playerList = new SortedSet<string>{"No Players Online"};
        public MainWindowContent()
        {
            InitializeComponent();
            instance = this;
            Players.ItemsSource = playerList;
            WebService webService = new WebService();
            pc = new PlayerCommandMenu();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
            Players.MouseRightButtonUp += Players_MouseRightButtonUp;
            MainWindowContent.instance.buttonStart.IsEnabled = true;
            MainWindowContent.instance.buttonStop.IsEnabled = false;
            MainWindowContent.instance.buttonRestart.IsEnabled = false;

        }

        private void Players_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            string name = (string)((TreeViewItem)sender).Items.CurrentItem;
            pc.setName(name);
            (sender as TreeViewItem).ContextMenu = pc;
        }
        

        private void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextOptions.SetTextFormattingMode(this, e.NewValue > 1.0 ? TextFormattingMode.Ideal : TextFormattingMode.Display);
        }
        public void addPlayer(string player)
        {
            if (playerList.Contains("No Players Online"))
            {
                playerList.Remove("No Players Online");
            }
            playerList.Add(player);
            Players.Items.Refresh();
        }
        public void removePlayer(string player)
        {
            playerList.Remove(player);
            if(playerList.Count == 0)
            {
                playerList.Add("No Players Online");
            }
            Players.Items.Refresh();
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (MinecraftServer.instance == null)
            {
                if (File.Exists(@"Server\data.msa"))
                {
                    new MinecraftServer(ServerConfiguration.deserializeFromXML(@"Server\data.msa"));
                }
                else
                {
                    new MinecraftServer(new ServerConfiguration());
                }
            }
            else
            {
                MinecraftServer.instance.startServer();
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            Minecraft_Server_Administrator.MainWindowContent.instance.Console.WriteInput("stop", Colors.White, false);
        }

        private void buttonWeather_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.IO.Path.Combine(Environment.GetEnvironmentVariable("JAVA_HOME"), "java.exe"));
        }

        private void buttonRestart_Click(object sender, RoutedEventArgs e)
        {
            MinecraftServer.instance.stopServer();
            while (MinecraftServer.instance.isRunning()) { }
            MinecraftServer.instance.startServer();
        }
    }
}
