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
using WPF_Flashcards.ViewModels;

namespace WPF_Flashcards.Views
{
    /// <summary>
    /// Interaction logic for AddDeckView.xaml
    /// </summary>
    public partial class AddDeckView : Window
    {
        public AddDeckView()
        {
            InitializeComponent();

            AddDeckViewModel addDeckViewModel = new AddDeckViewModel();
            this.DataContext = addDeckViewModel;

            // Navigate to the page when the application starts
            AddDeckFrame.Navigate(new AddDeckPageView());
        }
    }
}
