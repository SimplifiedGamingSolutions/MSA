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
        public ServerType type = ServerType.Forge;
        public string directory = "Server\\";
        public string serverFile = "";

        public ServerConfiguration()
        {
        }

        public enum ServerType
        {
            Vanilla,
            Forge,
            Bukkit
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