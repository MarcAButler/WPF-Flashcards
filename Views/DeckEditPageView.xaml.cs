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
        //public DeckEditPageView(Deck selectedDeck)
        public DeckEditPageView(MainViewModel viewModel)
        {
            InitializeComponent();
            //DataContext = selectedDeck;
            DataContext = viewModel;
        }

        private void NavigateToReviewDeckPage(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            Deck selectedDeck = viewModel.SelectedDeck;
            Card selectedCard = viewModel.SelectedCard;


            // Navigate to ReviewDeckPage and pass in deck Name and Description
            var reviewDeckPage = new ReviewDeckPageView(selectedDeck, selectedCard);
            NavigationService.Navigate(reviewDeckPage);
        }
    }
}
