using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Technewlogic.WpfDialogManagement;
using Technewlogic.WpfDialogManagement.Contracts;

namespace Minecraft_Server_Administrator
{
    

    public class PlayerCommandMenu : ContextMenu
    {
        ConsoleControl.WPF.ConsoleControl console;
        

        DialogManager dialogManager = new DialogManager(MainWindowContent.instance, MainWindowContent.instance.Dispatcher);
        MenuItem achievement, ban, banIp, kill, clear, deop, difficulty, effect, enchant, gamemode, give, kick, op, pardon, particle, playsound, replaceitem, scoreboard, setidletimeout, spreadplayers,stats,tell,tellraw,testfor,title,tp,trigger,whitelist,xp;
        string name;

        public PlayerCommandMenu(){
            console = MainWindowContent.instance.Console;
            this.name = "";
            

            achievement = new MenuItem(); achievement.Header = "achievement"; ban = new MenuItem(); ban.Header = "ban"; banIp = new MenuItem(); banIp.Header = "ban-ip"; kill = new MenuItem(); kill.Header = "kill"; clear = new MenuItem(); clear.Header = "clear"; deop = new MenuItem(); deop.Header = "deop"; difficulty = new MenuItem(); difficulty.Header = "difficulty"; effect = new MenuItem(); effect.Header = "effect"; enchant = new MenuItem(); enchant.Header = "enchant"; gamemode = new MenuItem(); gamemode.Header = "gamemode"; give = new MenuItem(); give.Header = "give"; kick = new MenuItem(); kick.Header = "kick"; op = new MenuItem(); op.Header = "op"; pardon = new MenuItem(); pardon.Header = "pardon"; tell = new MenuItem(); tell.Header = "tell"; give = new MenuItem(); give.Header = "give";
            
            this.Items.Add(achievement); this.Items.Add(ban); this.Items.Add(banIp); this.Items.Add(kill); this.Items.Add(clear); this.Items.Add(deop); this.Items.Add(difficulty); this.Items.Add(effect); this.Items.Add(enchant); this.Items.Add(gamemode); this.Items.Add(give); this.Items.Add(kick); this.Items.Add(op); this.Items.Add(pardon); this.Items.Add(tell);


            achievement.PreviewMouseLeftButtonUp += achievement_PreviewMouseLeftButtonUp;

        }

        public void setName(string name)
        {
            this.name = name;
        }

        void achievement_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IMessageDialog achievmentWindow = dialogManager.CreateMessageDialog("hello there \"" + name+"\"",DialogMode.Ok);

           // achievmentWindow.No = () => { MainWindowContent.instance.Console.WriteInput("kill conterio36", Colors.White, false); };
            achievmentWindow.Ok = () => { console.WriteInput("kill menoshin", Colors.White, false); };

            achievmentWindow.Show();

            


        }










    }
}
