using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minecraft_Server_Administrator
{
    

    class PlayerCommands
    {
        ConsoleControl.WPF.ConsoleControl console = MainWindowContent.instance.Console;

        PlayerCommands(){


            console.WriteInput("command", Colors.White, false);
        }



    }
}
