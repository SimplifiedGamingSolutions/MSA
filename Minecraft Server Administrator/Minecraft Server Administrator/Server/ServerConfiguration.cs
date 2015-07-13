using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Minecraft_Server_Administrator.Server
{
    public class ServerConfiguration
    {
        public static ServerConfiguration instance;
        public ServerType type = ServerType.Forge;
        public string serverDirectory = "Server\\";
        public string serverFile = "";
        public ServerProperties properties;

        public ServerConfiguration()
        {
            instance = this;
            properties = new ServerProperties();
        }

        public enum ServerType
        {
            Vanilla,
            Forge,
            Bukkit
        }
        public class ServerProperties
        {
            //server.properties

            public string motd = "A Minecraft Server";
            public int server_port = 25565;
            public string server_ip = "";
            public string level_seed = "";
            public string level_name = "world";
            public int gamemode = 0;
            public bool force_gamemode = false;
            public bool allow_nether = true;
            public int difficulty = 1;
            public bool spawn_monsters = true;
            public bool spawn_animals = true;
            public bool spawn_npcs = true;
            public bool generate_structures = true;
            public bool white_list = false;
            public bool pvp = true;
            public string generator_settings = "";
            public int spawn_protection = 16;
            public int max_tick_time = 60000;
            public bool enable_query = false;
            public int player_idle_timeout = 0;
            public int op_permission_level = 4;
            public string resource_pack_hash = "";
            public bool announce_player_achievements = true;
            public bool snooper_enabled = true;
            public string level_type = "";
            public bool hardcore = false;
            public bool enable_command_block = false;
            public int max_players = 20;
            public int network_compression_threshold = 256;
            public int max_world_size = 29999984;
            public bool allow_flight = false;
            public int view_distance = 10;
            public string resource_pack = "";
            public bool online_mode = true;
            public int max_build_height = 256;
            public bool enable_rcon = false;

            public ServerProperties()
            {
            }
        }

        public void serializeToXML(string destination)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(ServerConfiguration));
            System.IO.StreamWriter file = new System.IO.StreamWriter(destination);
            writer.Serialize(file, this);
            file.Close();
        }
        public static ServerConfiguration deserializeFromXML(string origin)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ServerConfiguration));
            System.IO.StreamReader file = new System.IO.StreamReader(origin);
            ServerConfiguration config = (ServerConfiguration)reader.Deserialize(file);
            return config;
        }

        internal static void loadProperties()
        {
            if(instance == null)
            {
                if (File.Exists(@"Server\data.msa"))
                {
                    new MinecraftServer(ServerConfiguration.deserializeFromXML(@"Server\data.msa"));
                }
                else
                {
                    new MinecraftServer(new ServerConfiguration());
                }
                createProperties();
            }
            else
            {

            }


        }

        private static void createProperties()
        {
            FieldInfo[] fields = ServerConfiguration.instance.properties.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                object obj = fields[i].GetValue(ServerConfiguration.instance.properties);
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
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition{ Height = GridLength.Auto});
            Label label = new Label();
            label.Name = name;
            label.Content = name;
            label.Width = 30;
            label.Height = 30;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add(new ComboBoxItem { Content = "False" });
            comboBox.Items.Add(new ComboBoxItem { Content = "True" });
            Grid.SetRow(comboBox, row);
            Grid.SetColumn(comboBox, 2);
            MainWindowContent.instance.ConfigGrid.Children.Add(comboBox);
        }

        private static void createStringProperty(string name, string value, int row)
        {
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            label.SetValue(Grid.ColumnProperty, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            TextBox textBox = new TextBox();
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.TextAlignment = TextAlignment.Left;
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 2);
            MainWindowContent.instance.ConfigGrid.Children.Add(textBox);
        }

        private static void createIntProperty(string name, int value, int row)
        {
            MainWindowContent.instance.ConfigGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Label label = new Label();
            label.Content = name;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 1);
            MainWindowContent.instance.ConfigGrid.Children.Add(label);
            //<TextBox Grid.Row="2" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
            TextBox textBox = new TextBox();
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.TextAlignment = TextAlignment.Left;
            textBox.PreviewTextInput += NumericOnly;
            DataObject.AddPastingHandler(textBox, TextBoxPasting);
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 2);
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
            /*<TextBox Grid.Row="6" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />*/
        }
        // Use the DataObject.Pasting Handler 
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
/*
             * 
                            <!-- Row 1-->
                            <Label HorizontalAlignment="Left" VerticalAlignment="Top" Content="Message of the Day" Grid.Row="1"/>
                            <TextBox Grid.Row="1" Grid.Column="2" VerticalAlignment="Center">
                            </TextBox>
                            <!-- Row 2-->
                            <Label Content="Server Port" Grid.Row="2"/>
                            <TextBox Grid.Row="2" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Server IP" Grid.Row="3"/>
                            <TextBox Grid.Row="3" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Level Seed" Grid.Row="4"/>
                            <TextBox Grid.Row="4" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Level Name" Grid.Row="5"/>
                            <TextBox Grid.Row="5" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Game Mode" Grid.Row="6"/>
                            <ComboBox Grid.Row="6" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>0</ComboBoxItem>
                                <ComboBoxItem>1</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 2-->
                            <Label Content="Force Game Mode" Grid.Row="7"/>
                            <ComboBox Grid.Row="7" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Allow Nether" Grid.Row="8"/>
                            <ComboBox Grid.Row="8" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 9-->
                            <Label Content="Difficulty" Grid.Row="9"/>
                            <ComboBox Grid.Row="9" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>0</ComboBoxItem>
                                <ComboBoxItem>1</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 2-->
                            <Label Content="Spawn Monsters" Grid.Row="10"/>
                            <ComboBox Grid.Row="10" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 9-->
                            <Label Content="Spawn Animals" Grid.Row="11"/>
                            <ComboBox Grid.Row="11" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 9-->
                            <Label Content="Spawn NPCs" Grid.Row="12"/>
                            <ComboBox Grid.Row="12" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 9-->
                            <Label Content="Generate Structures" Grid.Row="13"/>
                            <ComboBox Grid.Row="13" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 9-->
                            <Label Content="Spawn Protection Radius" Grid.Row="14"/>
                            <TextBox Grid.Row="14" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Max Tick Time" Grid.Row="15"/>
                            <TextBox Grid.Row="15" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Enable Query" Grid.Row="16"/>
                            <ComboBox Grid.Row="16" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Player Idle Time-Out" Grid.Row="17"/>
                            <TextBox Grid.Row="17" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Op Permission Level" Grid.Row="18"/>
                            <TextBox Grid.Row="18" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Resource Pack Hash" Grid.Row="19"/>
                            <TextBox Grid.Row="19" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Announce Player Achievements" Grid.Row="20"/>
                            <ComboBox Grid.Row="20" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Snooper Enabled" Grid.Row="21"/>
                            <ComboBox Grid.Row="21" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Level Type" Grid.Row="22"/>
                            <TextBox Grid.Row="22" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Hardcore" Grid.Row="23"/>
                            <ComboBox Grid.Row="23" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Enable Command Block" Grid.Row="24"/>
                            <ComboBox Grid.Row="24" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Max Players" Grid.Row="25"/>
                            <TextBox Grid.Row="25" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Network Compression Threshold" Grid.Row="26"/>
                            <TextBox Grid.Row="26" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Max World Size" Grid.Row="27"/>
                            <TextBox Grid.Row="27" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Allow Flight" Grid.Row="28"/>
                            <ComboBox Grid.Row="28" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <Label Content="View Distance" Grid.Row="29"/>
                            <TextBox Grid.Row="29" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Resource Pack" Grid.Row="30"/>
                            <TextBox Grid.Row="30" Grid.Column="2" VerticalAlignment="Center" />
                            <!-- Row 2-->
                            <Label Content="Online Mode" Grid.Row="31"/>
                            <ComboBox Grid.Row="31" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
                            <!-- Row 8-->
                            <Label Content="Max Build Height" Grid.Row="32"/>
                            <TextBox Grid.Row="32" Grid.Column="2" VerticalAlignment="Center" TextAlignment="Left" PreviewTextInput="NumericOnly"  DataObject.Pasting="TextBoxPasting" />
                            <!-- Row 3-->
                            <Label Content="Enable Rcon" Grid.Row="33"/>
                            <ComboBox Grid.Row="33" VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="2">
                                <ComboBoxItem>False</ComboBoxItem>
                                <ComboBoxItem>True</ComboBoxItem>
                            </ComboBox>
             */