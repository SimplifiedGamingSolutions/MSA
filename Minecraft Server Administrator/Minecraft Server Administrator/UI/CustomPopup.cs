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
    class CustomPopup : Popup
    {
        public CustomPopup(UIElement parent) : base()
        {
            this.Placement = PlacementMode.Center;
            this.PlacementTarget = MainWindowContent.instance;
            Grid grid = new Grid();
            grid.Height = 100;
            grid.Width = 200;
            Button button = new Button();
            button.Content = "Click me";
            button.PreviewMouseUp += button_PreviewMouseUp;
            grid.Children.Add(button);
            Border border = new Border();
            border.BorderBrush = Brushes.Red;
            border.BorderThickness = new Thickness(5, 5, 5, 5);
            border.Child = grid;
            this.Child = border;
            this.IsOpen = true;
        }

        void button_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.hide();
        }

        internal void show()
        {
            this.IsOpen = true;
        }

        internal void hide()
        {
            this.IsOpen = false;
        }
    }
}
