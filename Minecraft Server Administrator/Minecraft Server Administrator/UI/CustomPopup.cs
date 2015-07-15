using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Minecraft_Server_Administrator.UI
{
    class CustomPopup : Window
    {
        public CustomPopup(UIElement parent) : base()
        {
            Grid grid = new Grid();
            grid.Height = 100;
            grid.Width = 200;
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
    }
}
