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
            achievements.Add("fullBeacon", @"..\..\Resources\Images\Achievements\Grid_Beacon.png");
            achievements.Add("blazeRod", @"..\..\Resources\Images\Achievements\Grid_Blaze_rod.png");
            achievements.Add("killEnemy", @"..\..\Resources\Images\Achievements\Grid_Bone.png");
            achievements.Add("openInventory", @"..\..\Resources\Images\Achievements\Grid_Book.png");
            achievements.Add("bookcase", @"..\..\Resources\Images\Achievements\Grid_Bookshelf.png");
            achievements.Add("snipeSkeleton", @"..\..\Resources\Images\Achievements\Grid_Bow.png");
            achievements.Add("makeBread", @"..\..\Resources\Images\Achievements\Grid_Bread.png");
            achievements.Add("bakeCake", @"..\..\Resources\Images\Achievements\Grid_Cake.png");
            achievements.Add("cookFish", @"..\..\Resources\Images\Achievements\Grid_Cooked_Fish.png");
            achievements.Add("buildWorkBench", @"..\..\Resources\Images\Achievements\Grid_Crafting_Table.png");
            achievements.Add("exploreAllBiomes", @"..\..\Resources\Images\Achievements\Grid_Diamond_Boots.png");
            achievements.Add("diamonds", @"..\..\Resources\Images\Achievements\Grid_Diamond_Ore.png");
            achievements.Add("diamondsToYou", @"..\..\Resources\Images\Achievements\Grid_Diamond.png");
            achievements.Add("overkill", @"..\..\Resources\Images\Achievements\Grid_Diamond_Sword.png");
            achievements.Add("theEnd2", @"..\..\Resources\Images\Achievements\Grid_Dragon_Egg.png");
            achievements.Add("enchantments", @"..\..\Resources\Images\Achievements\Grid_Enchantment_Table.png");
            achievements.Add("theEnd", @"..\..\Resources\Images\Achievements\Grid_Eye_of_Ender.png");
            achievements.Add("buildFurnace", @"..\..\Resources\Images\Achievements\Grid_Furnace.png");
            achievements.Add("ghast", @"..\..\Resources\Images\Achievements\Grid_Ghast_Tear.png");
            achievements.Add("overpowered", @"..\..\Resources\Images\Achievements\Grid_Golden_Apple.png");
            achievements.Add("acquireIron", @"..\..\Resources\Images\Achievements\Grid_Iron_Ingot.png");
            achievements.Add("killCow", @"..\..\Resources\Images\Achievements\Grid_Leather.png");
            achievements.Add("potion", @"..\..\Resources\Images\Achievements\Grid_Mundane_Potion.png");
            achievements.Add("killWither", @"..\..\Resources\Images\Achievements\Grid_Nether_Star.png");
            achievements.Add("mineWood", @"..\..\Resources\Images\Achievements\Grid_Oak_Wood.png");
            achievements.Add("portal", @"..\..\Resources\Images\Achievements\Grid_Obsidian.png");
            achievements.Add("onARail", @"..\..\Resources\Images\Achievements\Grid_Rail.png");
            achievements.Add("flyPig", @"..\..\Resources\Images\Achievements\Grid_Saddle.png");
            achievements.Add("buildBetterPickaxe", @"..\..\Resources\Images\Achievements\Grid_Stone_Pickaxe.png");
            achievements.Add("breedCow", @"..\..\Resources\Images\Achievements\Grid_Wheat.png");
            achievements.Add("spawnWither", @"..\..\Resources\Images\Achievements\Grid_Wither_Skeleton_Skull.png");
            achievements.Add("buildHoe", @"..\..\Resources\Images\Achievements\Grid_Wooden_Hoe.png");
            achievements.Add("buildPickaxe", @"..\..\Resources\Images\Achievements\Grid_Wooden_Pickaxe.png");
            achievements.Add("buildSword", @"..\..\Resources\Images\Achievements\Grid_Wooden_Sword.png");
        }

        public BitmapImage getIcon(string achievement)
        {
            string path;
            achievements.TryGetValue(achievement, out path);
            string file = Path.GetFullPath(path);
            return new BitmapImage(new Uri(file));
        }
    }
}
