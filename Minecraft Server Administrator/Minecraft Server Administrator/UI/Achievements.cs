using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Minecraft_Server_Administrator.UI
{
    public class Achievements
    {
        Dictionary<String, String> achievements = new Dictionary<String, String>();

        public Achievements()
        {
            achievements.Add("fullBeacon", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Beacon.png");
            achievements.Add("blazeRod", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Blaze_rod.png");
            achievements.Add("killEnemy", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bone.png");
            achievements.Add("openInventory", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Book.png");
            achievements.Add("bookcase", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bookshelf.png");
            achievements.Add("snipeSkeleton", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bow.png");
            achievements.Add("makeBread", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bread.png");
            achievements.Add("bakeCake", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Cake.png");
            achievements.Add("cookFish", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Cooked_Fish.png");
            achievements.Add("buildWorkBench", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Crafting_Table.png");
            achievements.Add("exploreAllBiomes", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Boots.png");
            achievements.Add("diamonds", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Ore.png");
            achievements.Add("diamondsToYou", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond.png");
            achievements.Add("overkill", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Sword.png");
            achievements.Add("theEnd2", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Dragon_Egg.png");
            achievements.Add("enchantments", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Enchantment_Table.png");
            achievements.Add("theEnd", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Eye_of_Ender.png");
            achievements.Add("buildFurnace", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Furnace.png");
            achievements.Add("ghast", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Ghast_Tear.png");
            achievements.Add("overpowered", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Golden_Apple.png");
            achievements.Add("acquireIron", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Iron_Ingot.png");
            achievements.Add("killCow", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Leather.png");
            achievements.Add("potion", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Mundane_Potion.png");
            achievements.Add("killWither", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Nether_Star.png");
            achievements.Add("mineWood", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Oak_Wood.png");
            achievements.Add("portal", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Obsidian.png");
            achievements.Add("onARail", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Rail.png");
            achievements.Add("flyPig", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Saddle.png");
            achievements.Add("buildBetterPickaxe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Stone_Pickaxe.png");
            achievements.Add("breedCow", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wheat.png");
            achievements.Add("spawnWither", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wither_Skeleton_Skull.png");
            achievements.Add("buildHoe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Hoe.png");
            achievements.Add("buildPickaxe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Pickaxe.png");
            achievements.Add("buildSword", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Sword.png");
        }

        public BitmapImage getIcon(string achievement)
        {
            string path;
            achievements.TryGetValue(achievement, out path);
            return new BitmapImage(new Uri(path));
        }
    }
}
