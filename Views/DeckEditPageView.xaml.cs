using System;
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
using WPF_Flashcards.Models;
using WPF_Flashcards.ViewModels;

namespace WPF_Flashcards.Views
{
    /// <summary>
    /// Interaction logic for DeckEditPageView.xaml
    /// </summary>
    public partial class DeckEditPageView : Page
    {
        private MainViewModel ViewModel { get; set; }

        public DeckEditPageView(Deck selectedDeck)
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            //DataContext = selectedDeck;

            DataContext = ViewModel;
        }

        private void NavigateToReviewDeckPage(object sender, RoutedEventArgs e)
        {
            //Deck selectedDeck = (Deck)DataContext;
            // Navigate to ReviewDeckPage and pass in deck Name and Description
            var reviewDeckPage = new ReviewDeckPageView(ViewModel.SelectedDeck, ViewModel.SelectedCard);
            NavigationService.Navigate(reviewDeckPage);

            //NavigationService.Navigate(new ReviewDeckPageView());
        }


        

    }
}
