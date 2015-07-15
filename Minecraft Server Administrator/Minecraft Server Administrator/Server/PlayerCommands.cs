using Minecraft_Server_Administrator.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Technewlogic.WpfDialogManagement;
using Technewlogic.WpfDialogManagement.Contracts;

namespace Minecraft_Server_Administrator
{
    

    public class PlayerCommandMenu : ContextMenu
    {
        MenuItem achievement, 
            ban, banIp, kill, clear, 
            deop, difficulty, effect, 
            enchant, gamemode, give, 
            kick, op, pardon, particle, 
            playsound, replaceitem, 
            scoreboard, setidletimeout, 
            spreadplayers,stats,tell,
            tellraw,testfor,title,tp,
            trigger,whitelist,xp;

        public PlayerCommandMenu(){


            achievement = new MenuItem();
            achievement.Header = "achievement";
            achievement.PreviewMouseLeftButtonUp += achievement_PreviewMouseLeftButtonUp;
            this.Items.Add(achievement);

            ban = new MenuItem();
            ban.Header = "ban";
            ban.PreviewMouseUp += ban_PreviewMouseUp;
            this.Items.Add(ban);

            banIp = new MenuItem();
            banIp.Header = "ban-ip";
            banIp.PreviewMouseUp += banIp_PreviewMouseUp;
            this.Items.Add(banIp);

            kill = new MenuItem();
            kill.Header = "kill";
            kill.PreviewMouseUp += kill_PreviewMouseUp;
            this.Items.Add(kill);

            clear = new MenuItem();
            clear.Header = "clear";
            clear.PreviewMouseUp += clear_PreviewMouseUp;
            this.Items.Add(clear);

            deop = new MenuItem();
            deop.Header = "deop";
            deop.PreviewMouseUp += deop_PreviewMouseUp;
            this.Items.Add(deop);

            difficulty = new MenuItem();
            difficulty.Header = "difficulty";
            difficulty.PreviewMouseUp += difficulty_PreviewMouseUp;
            this.Items.Add(difficulty);

            effect = new MenuItem();
            effect.Header = "effect";
            effect.PreviewMouseUp += effect_PreviewMouseUp;
            this.Items.Add(effect);

            enchant = new MenuItem();
            enchant.Header = "enchant";
            enchant.PreviewMouseUp += enchant_PreviewMouseUp;
            this.Items.Add(enchant);

            gamemode = new MenuItem();
            gamemode.Header = "gamemode";
            gamemode.PreviewMouseUp += gamemode_PreviewMouseUp;
            this.Items.Add(gamemode);

            give = new MenuItem();
            give.Header = "give";
            give.PreviewMouseUp += give_PreviewMouseUp;
            this.Items.Add(give);

            kick = new MenuItem();
            kick.Header = "kick";
            kick.PreviewMouseUp += kick_PreviewMouseUp;
            this.Items.Add(kick);

            op = new MenuItem();
            op.Header = "op";
            op.PreviewMouseUp += op_PreviewMouseUp;
            this.Items.Add(op);

            pardon = new MenuItem();
            pardon.Header = "pardon";
            pardon.PreviewMouseUp += pardon_PreviewMouseUp;
            this.Items.Add(pardon);

            particle = new MenuItem();
            particle.Header = "particle";
            particle.PreviewMouseUp += particle_PreviewMouseUp;
            this.Items.Add(tell);

            playsound = new MenuItem();
            playsound.Header = "playsound";
            playsound.PreviewMouseUp += playsound_PreviewMouseUp;

            replaceitem = new MenuItem();
            replaceitem.Header = "replaceitem";
            replaceitem.PreviewMouseUp += replaceitem_PreviewMouseUp;

            scoreboard = new MenuItem();
            scoreboard.Header = "scoreboard";
            scoreboard.PreviewMouseUp += scoreboard_PreviewMouseUp;

            setidletimeout = new MenuItem();
            setidletimeout.Header = "setidletimeout";
            setidletimeout.PreviewMouseUp += setidletimeout_PreviewMouseUp;

            spreadplayers = new MenuItem();
            spreadplayers.Header = "spreadplayers";
            spreadplayers.PreviewMouseUp += spreadplayers_PreviewMouseUp;

            stats = new MenuItem();
            stats.Header = "stats";
            stats.PreviewMouseUp += stats_PreviewMouseUp;

            tell = new MenuItem();
            tell.Header = "tell";
            tell.PreviewMouseUp += tell_PreviewMouseUp;

            tellraw = new MenuItem();
            tellraw.Header = "tellraw";
            tellraw.PreviewMouseUp += tellraw_PreviewMouseUp;

            testfor = new MenuItem();
            testfor.Header = "testfor";
            testfor.PreviewMouseUp += testfor_PreviewMouseUp;

            title = new MenuItem();
            title.Header = "title";
            title.PreviewMouseUp += title_PreviewMouseUp;

            tp = new MenuItem();
            tp.Header = "tp";
            tp.PreviewMouseUp += tp_PreviewMouseUp;

            trigger = new MenuItem();
            trigger.Header = "trigger";
            trigger.PreviewMouseUp += trigger_PreviewMouseUp;

            whitelist = new MenuItem();
            whitelist.Header = "whitelist";
            whitelist.PreviewMouseUp += whitelist_PreviewMouseUp;

            xp = new MenuItem();
            xp.Header = "xp";
            xp.PreviewMouseUp += xp_PreviewMouseUp;
            
        }

        string getName()
        {

            IInputElement element = MainWindowContent.instance.Players.InputHitTest((Point)Tag);
            if (element is TextBlock)
            {
                return (element as TextBlock).Text;
            }
            return null;
            
        }

        void xp_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void whitelist_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void trigger_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void tp_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void title_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void testfor_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void tellraw_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void tell_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void stats_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void spreadplayers_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void setidletimeout_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void scoreboard_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void replaceitem_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void playsound_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void particle_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void pardon_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("pardon " + getName());
        }

        void op_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("op " + getName());
        }

        void kick_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("kick " + getName());
        }

        void give_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void gamemode_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void enchant_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("enchant " + getName());
        }

        void effect_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void difficulty_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void deop_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("deop " + getName());
        }

        void clear_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("clear " + getName());
        }

        void kill_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("kill " + getName());
        }

        void banIp_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ban_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinecraftServer.current.sendCommand("ban " + getName());
        }

        void achievement_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IMessageDialog achievmentWindow = MainWindowContent.dialogManager.CreateMessageDialog("hello there \"" + getName()+"\"",DialogMode.Ok);

            achievmentWindow.Ok = () => { MinecraftServer.current.sendCommand("kill menoshin"); };

            achievmentWindow.Show();

        }










    }
}
