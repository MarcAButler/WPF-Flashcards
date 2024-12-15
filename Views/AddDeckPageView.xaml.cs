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
using WPF_Flashcards.ViewModels;

namespace WPF_Flashcards.Views
{
    /// <summary>
    /// Interaction logic for AddDeckPageView.xaml
    /// </summary>
    public partial class AddDeckPageView : Page
    {

        private AddDeckViewModel ViewModel { get; set; }
        public AddDeckPageView()
        {
            InitializeComponent();
            ViewModel = new AddDeckViewModel();
            DataContext = ViewModel;
        }

        private void NavigateToAddCardsPage(object sender, RoutedEventArgs e)
        {
            // Navigate to AddCardsPageView and pass in deck Name and Description
            var addCardsPage = new AddCardsPageView(ViewModel.Name, ViewModel.Description);
            NavigationService.Navigate(addCardsPage);
        }
    }
}
