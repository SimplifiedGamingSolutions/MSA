using Minecraft_Server_Administrator.Server;
using Minecraft_Server_Administrator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using Technewlogic.WpfDialogManagement;

namespace Minecraft_Server_Administrator
{
    /// <summary>
    /// Interaction logic for MainWindowContent.xaml
    /// </summary>
    public partial class MainWindowContent : UserControl
    {
        public static DialogManager dialogManager;
        public static MainWindowContent instance;
        private readonly MainViewModel viewModel;
        private PlayerCommandMenu pc;
        private SortedSet<string> playerList = new SortedSet<string>{"No Players Online"};
        private List<string> historyList = new List<string>(10);
        public MainWindowContent()
        {
            InitializeComponent();
            instance = this;
            dialogManager = new DialogManager(this, Dispatcher);
            Players.ItemsSource = playerList;
            WebService webService = new WebService();
            pc = new PlayerCommandMenu();
            new MinecraftServer();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
            Players.MouseRightButtonUp += Players_MouseRightButtonUp;
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;
            commandTextBox.IsUndoEnabled = true;
            commandTextBox.KeyUp += commandTextBox_KeyUp;
            Console.LogChangedEvent += Console_LogChangedEvent;
            MainWindowContent.instance.buttonStart.IsEnabled = true;
            MainWindowContent.instance.buttonStop.IsEnabled = false;
            MainWindowContent.instance.buttonRestart.IsEnabled = false;
            Console.IsInputEnabled = true;
        }

        void Console_LogChangedEvent(string output)
        {
            allLog.AppendText(output);
            if (output.Contains("ERROR") || output.Contains("WARN"))
            {
                errorLog.AppendText(output);
            }
            else if(!output.Contains("CHAT"))
            {
                serverLog.AppendText(output);
            }
            else if(output.Contains("CHAT"))
            {
                chatLog.AppendText(output);
            }
        }
        private int currentHistory = 0;
        void commandTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox cmd = ((TextBox)sender);
            if(e.Key == Key.Enter)
            {
                historyList.Insert(0, cmd.Text);
                currentHistory = -1;
                Console.WriteInput(cmd.Text, Colors.White, false);
                cmd.Clear();
            }
            else if(e.Key == Key.Up)
            {
                if (historyList.Count-1 > currentHistory)
                    currentHistory++;
                cmd.Text = historyList.ElementAt(currentHistory);
            }
            else if(e.Key == Key.Down)
            {
                if (currentHistory > 0)
                    currentHistory--;
                cmd.Text = historyList.ElementAt(currentHistory);
            }
        }

        void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ServerPropertiesTab.IsSelected)
            {
                MinecraftServer.current.loadProperties();
                populatePropertiesTab(MinecraftServer.current);
            }
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
            MinecraftServer.current.startServer();
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
            MinecraftServer.current.stopServer();
            while (MinecraftServer.current.isRunning()) { }
            MinecraftServer.current.startServer();
        }

        private static void populatePropertiesTab(MinecraftServer server)
        {
            MainWindowContent.instance.ConfigGrid.Children.Clear();
            FieldInfo[] fields = server.props.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                object obj = fields[i].GetValue(server.props);
                if (obj is int)
                {
                    createIntProperty(fields[i].Name, (int)obj, i);
                }
                else if (obj is string)
                {
                    createStringProperty(fields[i].Name, (string)obj, i);
                }
                else if (obj is bool)
                {
                    createBooleanProperty(fields[i].Name, (bool)obj, i);
                }
            }

        }

        private static void createBooleanProperty(string name, bool value, int row)
        {
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 0);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add("false");
            comboBox.Items.Add("true");
            comboBox.VerticalAlignment = VerticalAlignment.Center;
            comboBox.HorizontalAlignment = HorizontalAlignment.Left;
            comboBox.SelectionChanged += comboBox_SelectionChanged;
            if (!value)
                comboBox.SelectedIndex = 0;
            else
                comboBox.SelectedIndex = 1;
            comboBox.Tag = new string[] { name.Replace('_', '-'), value.ToString().ToLower() };
            Grid.SetRow(comboBox, row);
            Grid.SetColumn(comboBox, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(comboBox);
        }

        static void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            if (box.Tag != null)
            {
                var str = File.ReadAllText(@"Server\server.properties");
                string old = ((string[])box.Tag)[0] + '=' + ((string[])box.Tag)[1];
                string newString = ((string[])box.Tag)[0] + '=' + box.SelectedItem.ToString().ToLower();
                str = str.Replace(old, newString);
                box.Tag = new string[] { ((string[])box.Tag)[0], box.SelectedItem.ToString().ToLower() };
                File.WriteAllText(@"Server\server.properties", str);
            }
        }

        private static void createStringProperty(string name, string value, int row)
        {
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 0);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            TextBox textBox = new TextBox();
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.TextAlignment = TextAlignment.Left;
            textBox.Text = value;
            textBox.TextChanged += textBox_TextChanged;
            textBox.Tag = new string[] { name.Replace('_', '-'), value.ToLower() };
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(textBox);
        }

        static void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Tag != null)
            {
                var str = File.ReadAllText(@"Server\server.properties");
                string old = ((string[])box.Tag)[0] + '=' + ((string[])box.Tag)[1];
                string newString = ((string[])box.Tag)[0] + '=' + box.Text.ToLower();
                str = str.Replace(old, newString);
                box.Tag = new string[] { ((string[])box.Tag)[0], box.Text.ToLower() };
                File.WriteAllText(@"Server\server.properties", str);
            }
        }

        private static void createIntProperty(string name, int value, int row)
        {
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 0);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            TextBox textBox = new TextBox();
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.TextAlignment = TextAlignment.Left;
            textBox.PreviewTextInput += NumericOnly;
            textBox.Text = value.ToString();
            textBox.TextChanged += textBox_TextChanged;
            textBox.Tag = new string[] { name.Replace('_', '-'), value.ToString() };
            DataObject.AddPastingHandler(textBox, TextBoxPasting);
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(textBox);
        }

        static void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private static void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
