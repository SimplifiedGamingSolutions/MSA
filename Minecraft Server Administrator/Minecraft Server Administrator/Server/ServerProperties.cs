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

        public ServerProperties(string path)
        {
            loadProperties(path);
        }

        public void loadProperties(string path)
        {
            StreamReader reader = File.OpenText(path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split('=');
                if (items.Length.Equals(2) && !items[0].Contains("#"))
                {
                    FieldInfo field = this.GetType().GetField(items[0].Replace('-', '_'));
                    if (field.GetValue(this) is int)
                    {
                        field.SetValue(this, int.Parse(items[1]));
                    }
                    else if (field.GetValue(this) is string)
                    {
                        field.SetValue(this, items[1]);
                    }
                    else if (field.GetValue(this) is bool)
                    {
                        field.SetValue(this, bool.Parse(items[1]));
                    }
                }
            }
        }
    }
}
