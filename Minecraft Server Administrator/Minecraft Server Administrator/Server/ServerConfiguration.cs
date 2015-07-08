using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Administrator.Server
{
    public class ServerConfiguration
    {
        public ServerType type;
        public string directory;
        public ServerProperties properties;

        public ServerConfiguration()
        {
            type = ServerType.Forge;
            directory = "Server\\";
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
            int spawn_protection { get; set; }
            int max_tick_time { get; set; }
            string generator_settings { get; set; }
            bool force_gamemode { get; set; }
            bool allow_nether { get; set; }
            int gamemode { get; set; }
            bool enable_query { get; set; }
            int player_idle_timeout { get; set; }
            int difficulty { get; set; }
            bool spawn_monsters { get; set; }
            int op_permission_level { get; set; }
            string resource_pack_hash { get; set; }
            bool announce_player_achievements { get; set; }
            bool pvp { get; set; }
            bool snooper_enabled { get; set; }
            string level_type { get; set; }
            bool hardcore { get; set; }
            bool enable_command_block { get; set; }
            int max_players { get; set; }
            int network_compression_threshold { get; set; }
            int max_world_size { get; set; }
            int server_port { get; set; }
            string server_ip { get; set; }
            bool spawn_npcs { get; set; }
            bool allow_flight { get; set; }
            string level_name { get; set; }
            int view_distance { get; set; }
            string resource_pack { get; set; }
            bool spawn_animals { get; set; }
            bool white_list { get; set; }
            bool generate_structures { get; set; }
            bool online_mode { get; set; }
            int max_build_height { get; set; }
            string level_seed { get; set; }
            string motd { get; set; }
            bool enable_rcon { get; set; }

            public ServerProperties()
            {
                spawn_protection = 16;
                max_tick_time = 60000;
                generator_settings = "";
                force_gamemode = false;
                allow_nether = true;
                gamemode = 0;
                enable_query = false;
                player_idle_timeout = 0;
                difficulty = 1;
                spawn_monsters = true;
                op_permission_level = 4;
                resource_pack_hash = "";
                announce_player_achievements = true;
                pvp = true;
                snooper_enabled = true;
                level_type = "";
                hardcore = false;
                enable_command_block = false;
                max_players = 20;
                network_compression_threshold = 256;
                max_world_size = 29999984;
                server_port = 25565;
                server_ip = "";
                spawn_npcs = true;
                allow_flight = false;
                level_name = "world";
                view_distance = 10;
                resource_pack = "";
                spawn_animals = true;
                white_list = false;
                generate_structures = true;
                online_mode = true;
                max_build_height = 256;
                level_seed = "";
                motd = "A Minecraft Server";
                enable_rcon = false;
            }
        }
    }
}
