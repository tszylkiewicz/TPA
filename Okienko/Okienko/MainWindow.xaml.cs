﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Model.ViewModel;

namespace WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext  = new MyViewModel()
            {
                BrowseFile = new Okienko.View.BrowseFile()
            };          
        }
    }
}
