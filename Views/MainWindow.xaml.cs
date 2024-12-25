using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Flashcards.Services;
using WPF_Flashcards.ViewModels;

namespace WPF_Flashcards.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Make an instance of MainViewModel to manage input
            MainViewModel mainViewModel = new MainViewModel();
            // Set the default context for binding
            // - Allows access to decks with bindings
            this.DataContext = mainViewModel;

            NavigationService.MainFrame = MainFrame;

            // Navigate to the page when the application starts
            MainFrame.Navigate(new IntroductionPageView());
        }
    }
}