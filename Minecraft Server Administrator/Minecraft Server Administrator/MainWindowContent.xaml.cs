using Minecraft_Server_Administrator.Server;
using Minecraft_Server_Administrator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private SortedSet<string> playerList = new SortedSet<string>();
        public MainWindowContent()
        {
            InitializeComponent();
            instance = this;
            PlayersListBox.ItemsSource = playerList;
            WebService webService = new WebService();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
        }
        private void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextOptions.SetTextFormattingMode(this, e.NewValue > 1.0 ? TextFormattingMode.Ideal : TextFormattingMode.Display);
        }
        public void addPlayer(string player)
        {
            playerList.Add(player);
            PlayersListBox.Items.Refresh();
        }
        public void removePlayer(string player)
        {
            playerList.Remove(player);
            PlayersListBox.Items.Refresh();
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            new MinecraftServer(new ServerConfiguration());
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonRestart_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
