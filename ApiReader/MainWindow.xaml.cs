﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ApiReader.Core;

namespace ApiReader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ControllerPath.Text =
                @"C:\Users\lucas\Documents\Visual Studio 2013\Projects\Gabbler\Gabbler.gApi\Controllers";
        ModelPath.Text =
                @"C:\Users\lucas\Documents\Visual Studio 2013\Projects\Gabbler\Gabbler.gApi\Models";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            FindControllers fc = new FindControllers(ControllerPath.Text,ModelPath.Text);
            Controlers.ItemsSource = fc.Controlers;
        }
    }
}
