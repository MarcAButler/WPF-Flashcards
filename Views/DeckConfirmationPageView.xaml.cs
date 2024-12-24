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
    public partial class DeckConfirmationPageView : Page
    {
        //public DeckConfirmationPageView(Deck selectedDeck)
        public DeckConfirmationPageView(MainViewModel viewModel)
        {
            InitializeComponent();
            //DataContext = selectedDeck;
            DataContext = viewModel;
        }

        private void NavigateToReviewDeckPage(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            //Deck selectedDeck = viewModel.SelectedDeck;
            //Card selectedCard = viewModel.SelectedCard;
            //ReviewState currentReviewState = viewModel.CurrentReviewState;

            // Navigate to ReviewDeckPage and pass in deck Name and Description
            //var reviewDeckPage = new ReviewDeckPageView(selectedDeck, selectedCard, currentReviewState);
            var reviewDeckPage = new ReviewDeckPageView(viewModel);
            NavigationService.Navigate(reviewDeckPage);
        }

        private void NavigateToEditDeckPage(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;


            NavigationService.Navigate(new DeckEditPageView(viewModel));
        }


    }
}
