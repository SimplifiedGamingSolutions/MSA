using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minecraft_Server_Administrator
{
    

    public class PlayerCommands
    {
        ConsoleControl.WPF.ConsoleControl console = MainWindowContent.instance.Console;

        public PlayerCommands(){
            
        }


        public void processRightClick(string player)
        {
            Console.Write(player);
        }








    }
}
