using Minecraft_Server_Administrator.ViewModels;
using Minecraft_Server_Administrator.WebService;
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
        private readonly MainViewModel viewModel;
        public static BindingList<string> playerList = new BindingList<string>();
        public MainWindowContent()
        {
            InitializeComponent();
            PlayersListBox.ItemsSource = playerList;
            Server server = new Server();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
        }
        private void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextOptions.SetTextFormattingMode(this, e.NewValue > 1.0 ? TextFormattingMode.Ideal : TextFormattingMode.Display);
        }
    }
}
