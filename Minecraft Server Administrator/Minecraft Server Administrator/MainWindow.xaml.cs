﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minecraft_Server_Administrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.StateChanged += MainWindowContent_StateChanged;
            this.IsVisibleChanged += MainWindow_IsVisibleChanged;
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Minecraft Server Administrator;component/Resources/Images/SGSLogo.png"));
        }

        void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
        }

        void MainWindowContent_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    break;
                case WindowState.Minimized:
                    this.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case WindowState.Normal:
                    break;
            }
        }

        void Tray_Open(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Visible;
        }

        void Tray_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void Tray_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ZoomSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextOptions.SetTextFormattingMode(this, e.NewValue > 1.0 ? TextFormattingMode.Ideal : TextFormattingMode.Display);
        }
    }

}
