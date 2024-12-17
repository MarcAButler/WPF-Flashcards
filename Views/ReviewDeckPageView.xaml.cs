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
    /// Interaction logic for ReviewDeckPageView.xaml
    /// </summary>
    public partial class ReviewDeckPageView : Page
    {
        //public ReviewDeckPageView(Deck selectedDeck, Card? selectedCard, ReviewState currentReviewState)
        public ReviewDeckPageView(MainViewModel viewModel)
        {
            InitializeComponent();

            //DataContext = new MainViewModel();
            //DataContext = new
            //{
            //    Deck = selectedDeck,
            //    Card = selectedCard,
            //    CurrentReviewState = currentReviewState
            //};
            DataContext = viewModel;

        }
    }
}
