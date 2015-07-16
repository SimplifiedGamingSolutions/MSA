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
        Grid grid;
        public CustomPopup(UIElement parent) : base()
        {
            grid = new Grid();
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
            Button button = new Button();
            button.Content = "Click me";
            button.PreviewMouseUp += button_PreviewMouseUp;
            grid.Children.Add(button);
            this.Content = grid;
        }

        void button_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
        
        public void selectAchievement()
        {
            grid.Children.Clear();
            Achievements achievements = new Achievements();
            this.addItem("openInventory", achievements.getIcon("openInventory"), "Taking Inventory", 0, 0);
            this.addItem("mineWood", achievements.getIcon("mineWood"), "Getting Wood", 0, 1);
            this.addItem("buildWorkBench", achievements.getIcon("buildWorkBench"), "Benchmarking", 0, 2);
            this.addItem("buildPickaxe", achievements.getIcon("buildPickaxe"), "Time to Mine!", 0, 3);
            this.addItem("buildFurnace", achievements.getIcon("buildFurnace"), "Hot Topic", 0, 4);
            this.addItem("acquireIron", achievements.getIcon("acquireIron"), "Acquire Hardware", 1, 0);
            this.addItem("buildHoe", achievements.getIcon("buildHoe"), "Time to Farm!", 1, 1);
            this.addItem("makeBread", achievements.getIcon("makeBread"), "Bake Bread", 1, 2);
            this.addItem("bakeCake", achievements.getIcon("bakeCake"), "The Lie", 1, 3);
            this.addItem("buildBetterPickaxe", achievements.getIcon("buildBetterPickaxe"), "Getting an Upgrade", 1, 4);
            this.addItem("cookFish", achievements.getIcon("cookFish"), "Delicious Fish", 2, 0);
            this.addItem("onARail", achievements.getIcon("onARail"), "On A Rail", 2, 1);
            this.addItem("buildSword", achievements.getIcon("buildSword"), "Time to Strike!", 2, 3);
            this.addItem("killEnemy", achievements.getIcon("killEnemy"), "Monster Hunter", 2, 4);
            this.addItem("killCow", achievements.getIcon("killCow"), "Cow Tipper", 3, 0);
            this.addItem("flyPig", achievements.getIcon("flyPig"), "When Pigs Fly", 3, 1);
            this.addItem("snipeSkeleton", achievements.getIcon("snipeSkeleton"), "Sniper Duel", 3, 2);
            this.addItem("diamonds", achievements.getIcon("diamonds"), "DIAMONDS!", 3, 3);
            this.addItem("portal", achievements.getIcon("portal"), "We Need to Go Deeper", 3, 4);
            this.addItem("ghast", achievements.getIcon("ghast"), "Return to Sender", 4, 0);
            this.addItem("blazeRod", achievements.getIcon("blazeRod"), "Into Fire", 4, 1);
            this.addItem("potion", achievements.getIcon("potion"), "Local Brewery", 4, 2);
            this.addItem("theEnd", achievements.getIcon("theEnd"), "The End?", 4, 3);
            this.addItem("theEnd2", achievements.getIcon("theEnd2"), "The End.", 4, 4);
            this.addItem("enchantments", achievements.getIcon("enchantments"), "Enchanter", 5, 0);
            this.addItem("overkill", achievements.getIcon("overkill"), "Overkill", 5, 1);
            this.addItem("bookcase", achievements.getIcon("bookcase"), "Librarian", 5, 2);
            this.addItem("exploreAllBiomes", achievements.getIcon("exploreAllBiomes"), "Adventuring Time", 5, 3);
            this.addItem("spawnWither", achievements.getIcon("spawnWither"), "The Beginning?", 5, 4);
            this.addItem("killWither", achievements.getIcon("killWither"), "The Beginning.", 6, 0);
            this.addItem("fullBeacon", achievements.getIcon("fullBeacon"), "Beaconator", 6, 1);
            this.addItem("breedCow", achievements.getIcon("breedCow"), "Repopulation", 6, 2);
            this.addItem("diamondsToYou", achievements.getIcon("diamondsToYou"), "Diamonds to you!", 6, 3);
            this.addItem("overpowered", achievements.getIcon("overpowered"), "Overpowered", 6, 4);
            ShowDialog();
        }

        private void addItem(string name, BitmapImage image, string message, int row, int column)
        {
            Image uiimage = new Image();
            uiimage.Stretch = Stretch.Uniform;
            Grid.SetColumn(uiimage, column);
            Grid.SetRow(uiimage, row);
            uiimage.Source = image;
            grid.Children.Add(uiimage);
        }
    }
}
