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
        static Dictionary<String, String> achievements = new Dictionary<String, String> { 
            {"fullBeacon", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Beacon.png"},
            {"blazeRod", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Blaze_rod.png"},
            {"killEnemy", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bone.png"},
            {"openInventory", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Book.png"},
            {"bookcase", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bookshelf.png"},
            {"snipeSkeleton", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bow.png"},
            {"makeBread", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Bread.png"},
            {"bakeCake", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Cake.png"},
            {"cookFish", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Cooked_Fish.png"},
            {"buildWorkBench", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Crafting_Table.png"},
            {"exploreAllBiomes", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Boots.png"},
            {"diamonds", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Ore.png"},
            {"diamondsToYou", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond.png"},
            {"overkill", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Diamond_Sword.png"},
            {"theEnd2", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Dragon_Egg.png"},
            {"enchantments", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Enchantment_Table.png"},
            {"theEnd", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Eye_of_Ender.png"},
            {"buildFurnace", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Furnace.png"},
            {"ghast", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Ghast_Tear.png"},
            {"overpowered", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Golden_Apple.png"},
            {"acquireIron", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Iron_Ingot.png"},
            {"killCow", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Leather.png"},
            {"potion", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Mundane_Potion.png"},
            {"killWither", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Nether_Star.png"},
            {"mineWood", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Oak_Wood.png"},
            {"portal", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Obsidian.png"},
            {"onARail", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Rail.png"},
            {"flyPig", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Saddle.png"},
            {"buildBetterPickaxe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Stone_Pickaxe.png"},
            {"breedCow", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wheat.png"},
            {"spawnWither", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wither_Skeleton_Skull.png"},
            {"buildHoe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Hoe.png"},
            {"buildPickaxe", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Pickaxe.png"},
            {"buildSword", "pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/Achievements/Grid_Wooden_Sword.png"} };

        public static BitmapImage getIcon(string achievement)
        {
            string path;
            achievements.TryGetValue(achievement, out path);
            return new BitmapImage(new Uri(path));
        }
    }
}
