using Minecraft_Server_Administrator.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minecraft_Server_Administrator.UI
{
    class CustomPopup : Window
    {
        Grid grid { get; set; }
        string player_name { get; set; }

        public CustomPopup(string player_name) : base()
        {
            grid = new Grid();
            this.Content = grid;
            this.player_name = player_name;
            this.SizeChanged += CustomPopup_SizeChanged;
        }

        public static AchievementPopup createAchievementPopup(string player_name, Minecraft_Server_Administrator.UI.CustomPopup.AchievementPopup.Achievement action)
        {
            return new AchievementPopup(player_name, action);

        }

        public static EffectPopup createEffectPopup(string player_name)
        {
            return new EffectPopup(player_name);
        }

        void CustomPopup_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //this.Title = String.Concat("Height: ", ActualHeight, "Width: ", ActualWidth);
        }
        
        public class EffectPopup : CustomPopup
        {
            ComboBox effectBox = new ComboBox();
            TextBox durationBox = new TextBox();
            TextBox amplifierBox = new TextBox();
            ComboBox hideParticlesBox = new ComboBox();
            public EffectPopup(string player_name) : base(player_name)
            {
                this.Title = "Give '" + player_name + "' effect.";
                this.Height = 110;
                this.Width = 550;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Label label1 = new Label { Content = "Effect" };
                Grid.SetRow(label1, 0);
                Grid.SetColumn(label1, 0);
                grid.Children.Add(label1);
                effectBox.VerticalAlignment = VerticalAlignment.Center;
                effectBox.Items.Add(new ComboBoxItem { Content = "effect" });
                Grid.SetRow(effectBox, 0);
                Grid.SetColumn(effectBox, 1);
                grid.Children.Add(effectBox);

                Label label2 = new Label { Content = "(Optional)\nSeconds" };
                Grid.SetRow(label2, 0);
                Grid.SetColumn(label2, 2);
                grid.Children.Add(label2);
                durationBox.VerticalAlignment = VerticalAlignment.Center;
                durationBox.TextAlignment = TextAlignment.Left;
                durationBox.PreviewTextInput += MainWindowContent.NumericOnly;
                DataObject.AddPastingHandler(durationBox, MainWindowContent.TextBoxPasting);
                Grid.SetRow(durationBox, 0);
                Grid.SetColumn(durationBox, 3);
                grid.Children.Add(durationBox);

                Label label3 = new Label { Content = "(Optional)\nAmplifier" };
                Grid.SetRow(label3, 0);
                Grid.SetColumn(label3, 4);
                grid.Children.Add(label3);
                amplifierBox.VerticalAlignment = VerticalAlignment.Center;
                amplifierBox.TextAlignment = TextAlignment.Left;
                amplifierBox.PreviewTextInput += MainWindowContent.NumericOnly;
                DataObject.AddPastingHandler(amplifierBox, MainWindowContent.TextBoxPasting);
                Grid.SetRow(amplifierBox, 0);
                Grid.SetColumn(amplifierBox, 5);
                grid.Children.Add(amplifierBox);

                Label label4 = new Label { Content = "(Optional)\nhideParticles" };
                Grid.SetRow(label4, 0);
                Grid.SetColumn(label4, 6);
                grid.Children.Add(label4);
                hideParticlesBox.VerticalAlignment = VerticalAlignment.Center;
                hideParticlesBox.Items.Add(new ComboBoxItem { Content = true });
                hideParticlesBox.Items.Add(new ComboBoxItem { Content = false });
                Grid.SetRow(hideParticlesBox, 0);
                Grid.SetColumn(hideParticlesBox, 7);
                grid.Children.Add(hideParticlesBox);

                Button button = new Button{ Content = "Send" };
                button.Click += button_Click;
                Grid.SetRow(button, 1);
                Grid.SetColumnSpan(button, 8);
                button.VerticalAlignment = VerticalAlignment.Center;
                button.Width = 50;
                grid.Children.Add(button);

            }

            void button_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show(String.Concat("effect ", player_name, " ", durationBox.Text, " ", (effectBox.SelectedItem as ComboBoxItem).Content, " ", amplifierBox.Text, " ", (hideParticlesBox.SelectedItem as ComboBoxItem).Content));
                MinecraftServer.current.sendCommand(String.Concat("effect ", player_name, " ", durationBox.Text, " ", (effectBox.SelectedItem as ComboBoxItem).Content, " ", amplifierBox.Text, " ", (hideParticlesBox.SelectedItem as ComboBoxItem).Content));
                this.Close();
            }

        }

        public class AchievementPopup : CustomPopup
        {
            Achievement action { get; set; }
            public AchievementPopup(string player_name, Achievement action) : base(player_name)
            {
                this.action = action;
                if (action == Achievement.GIVE)
                    this.Title = String.Concat("Give '", player_name, "' Achievement");
                else if (action == Achievement.TAKE)
                    this.Title = String.Concat("Give '", player_name, "' Achievement");
                this.Height = 482;
                //this.MinHeight = 482;
                this.Width = 628;
                //this.MinWidth = 628;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                //grid.Background = Brushes.Black;
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                this.addButton("openInventory", Achievements.getIcon("openInventory"), "Taking Inventory", 0, 0);
                this.addButton("mineWood", Achievements.getIcon("mineWood"), "Getting Wood", 0, 1);
                this.addButton("buildWorkBench", Achievements.getIcon("buildWorkBench"), "Benchmarking", 0, 2);
                this.addButton("buildPickaxe", Achievements.getIcon("buildPickaxe"), "Time to Mine!", 0, 3);
                this.addButton("buildFurnace", Achievements.getIcon("buildFurnace"), "Hot Topic", 0, 4);
                this.addButton("acquireIron", Achievements.getIcon("acquireIron"), "Acquire Hardware", 1, 0);
                this.addButton("buildHoe", Achievements.getIcon("buildHoe"), "Time to Farm!", 1, 1);
                this.addButton("makeBread", Achievements.getIcon("makeBread"), "Bake Bread", 1, 2);
                this.addButton("bakeCake", Achievements.getIcon("bakeCake"), "The Lie", 1, 3);
                this.addButton("buildBetterPickaxe", Achievements.getIcon("buildBetterPickaxe"), "Getting an Upgrade", 1, 4);
                this.addButton("cookFish", Achievements.getIcon("cookFish"), "Delicious Fish", 2, 0);
                this.addButton("onARail", Achievements.getIcon("onARail"), "On A Rail", 2, 1);
                this.addButton("buildSword", Achievements.getIcon("buildSword"), "Time to Strike!", 2, 2);
                this.addButton("killEnemy", Achievements.getIcon("killEnemy"), "Monster Hunter", 2, 3);
                this.addButton("killCow", Achievements.getIcon("killCow"), "Cow Tipper", 2, 4);
                this.addButton("flyPig", Achievements.getIcon("flyPig"), "When Pigs Fly", 3, 0);
                this.addButton("snipeSkeleton", Achievements.getIcon("snipeSkeleton"), "Sniper Duel", 3, 1);
                this.addButton("diamonds", Achievements.getIcon("diamonds"), "DIAMONDS!", 3, 2);
                this.addButton("overpowered", Achievements.getIcon("overpowered"), "Overpowered", 3, 3);
                this.addButton("portal", Achievements.getIcon("portal"), "We Need to Go Deeper", 3, 4);
                this.addButton("ghast", Achievements.getIcon("ghast"), "Return to Sender", 4, 0);
                this.addButton("blazeRod", Achievements.getIcon("blazeRod"), "Into Fire", 4, 1);
                this.addButton("potion", Achievements.getIcon("potion"), "Local Brewery", 4, 2);
                this.addButton("theEnd", Achievements.getIcon("theEnd"), "The End?", 4, 3);
                this.addButton("theEnd2", Achievements.getIcon("theEnd2"), "The End.", 4, 4);
                this.addButton("enchantments", Achievements.getIcon("enchantments"), "Enchanter", 5, 0);
                this.addButton("overkill", Achievements.getIcon("overkill"), "Overkill", 5, 1);
                this.addButton("bookcase", Achievements.getIcon("bookcase"), "Librarian", 5, 2);
                this.addButton("exploreAllBiomes", Achievements.getIcon("exploreAllBiomes"), "Adventuring Time", 5, 3);
                this.addButton("spawnWither", Achievements.getIcon("spawnWither"), "The Beginning?", 5, 4);
                this.addButton("killWither", Achievements.getIcon("killWither"), "The Beginning.", 6, 0);
                this.addButton("fullBeacon", Achievements.getIcon("fullBeacon"), "Beaconator", 6, 1);
                this.addButton("breedCow", Achievements.getIcon("breedCow"), "Repopulation", 6, 2);
                this.addButton("diamondsToYou", Achievements.getIcon("diamondsToYou"), "Diamonds to you!", 6, 3);
            }

            public enum Achievement
            {
                GIVE, TAKE
            }

            private void addButton(string name, BitmapImage image, string message, int row, int column)
            {
                Image uiimage = new Image();
                //uiimage.Stretch = Stretch.Uniform;
                uiimage.Height = 32;
                uiimage.Width = 32;
                uiimage.Source = image;
                Button button = new Button();
                //button.Background = Brushes.DarkGreen;
                StackPanel panel = new StackPanel();
                panel.Children.Add(uiimage);
                Label label = new Label();
                label.Content = message;
                panel.Children.Add(label);
                button.Content = panel;
                button.Tag = name;
                button.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                button.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                button.Click += achievement_Button_Click;
                StackPanel stack = new StackPanel();
                stack.Children.Add(button);
                //stack.Children.Add(new Label { Content = message });
                stack.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                stack.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                Grid.SetColumn(stack, column);
                Grid.SetRow(stack, row);
                grid.Children.Add(stack);
            }


            void achievement_Button_Click(object sender, RoutedEventArgs e)
            {
                if (action == Achievement.GIVE)
                    MinecraftServer.current.sendCommand("achievement give achievement." + (sender as Button).Tag + " " + base.player_name);
                else if (action == Achievement.TAKE)
                    MinecraftServer.current.sendCommand("achievement take achievement." + (sender as Button).Tag + " " + base.player_name);
                this.Close();
            }
        }
    }
}
