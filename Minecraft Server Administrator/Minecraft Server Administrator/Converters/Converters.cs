using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Minecraft_Server_Administrator.Converters
{
    public class TabSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            TabControl tabControl = values[0] as TabControl;
            double width = tabControl.ActualWidth / tabControl.Items.Count;
            //Subtract 1, otherwise we could overflow to two rows.
            return (width <= 1) ? 0 : (width - 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class RelativeWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Grid dockPanel = values[0] as Grid;
            return dockPanel.ActualWidth * (double)values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class RelativeHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Grid dockPanel = values[0] as Grid;
            return dockPanel.ActualHeight * (double)values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
