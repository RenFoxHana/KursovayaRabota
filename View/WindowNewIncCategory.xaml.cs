﻿using System.Windows;

namespace Version3.View
{
    public partial class WindowNewIncCategory : Window
    {
        public WindowNewIncCategory()
        {
            InitializeComponent();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
