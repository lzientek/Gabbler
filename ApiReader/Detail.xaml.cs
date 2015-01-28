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
using System.Windows.Shapes;
using ApiReader.Core;

namespace ApiReader
{
    /// <summary>
    /// Logique d'interaction pour Detail.xaml
    /// </summary>
    public partial class Detail : Window
    {
        public Method Method { get; set; }
        public MethodModels MethodsModel { get; set; }

        public Detail(IEnumerable<Model> models,Method method)
        {
            InitializeComponent();
            
            Method = method;
            MethodsModel = method.FindMethodsModels(models);

            DataContext = this;
        }

    }
}
