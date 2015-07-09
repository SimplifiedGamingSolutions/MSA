using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Administrator.Server
{
    public class ServerConfiguration
    {
        public ServerType type = ServerType.Forge;
        public string serverDirectory = "Server\\";
        public string serverFile = "";
        public ServerProperties properties;

        public ServerConfiguration()
        {
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
            
            public int spawn_protection = 16;
            public int max_tick_time = 60000;
            public string generator_settings = "";
            public bool force_gamemode = false;
            public bool allow_nether = true;
            public int gamemode = 0;
            public bool enable_query = false;
            public int player_idle_timeout = 0;
            public int  difficulty = 1;
            public bool spawn_monsters = true;
            public int op_permission_level = 4;
            public string resource_pack_hash = "";
            public bool announce_player_achievements = true;
            public bool pvp = true;
            public bool snooper_enabled = true;
            public string level_type = "";
            public bool hardcore = false;
            public bool enable_command_block = false;
            public int max_players = 20;
            public int network_compression_threshold = 256;
            public int max_world_size = 29999984;
            public int server_port = 25565;
            public string server_ip = "";
            public bool spawn_npcs = true;
            public bool allow_flight = false;
            public string level_name = "world";
            public int view_distance = 10;
            public string resource_pack = "";
            public bool spawn_animals = true;
            public bool white_list = false;
            public bool generate_structures = true;
            public bool online_mode = true;
            public int max_build_height = 256;
            public string level_seed = "";
            public string motd = "A Minecraft Server";
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
    }
}
