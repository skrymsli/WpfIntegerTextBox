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

namespace WpfIntegerTextBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            "Number", typeof (int), typeof (MainWindow), new PropertyMetadata(default(int)));

        public int Number
        {
            get { return (int) GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof (int), typeof (MainWindow), new PropertyMetadata(default(int)));

        public int Max
        {
            get { return (int) GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof (int), typeof (MainWindow), new PropertyMetadata(default(int)));

        public int Min
        {
            get { return (int) GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

    }
}
