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
using System.Xml.Linq;
using WPF_Flashcards.ViewModels;

namespace WPF_Flashcards.Views
{
    /// <summary>
    /// Interaction logic for AddCardsPageView.xaml
    /// </summary>
    public partial class AddCardsPageView : Page
    {
        private string? DeckName;
        private string? DeckDescription;



        public AddCardsPageView(string deckName, string deckDescription)
        {
            InitializeComponent();

            //DeckName = deckName;
            //DeckDescription = deckDescription;

            DataContext = new AddDeckViewModel(deckName, deckDescription);
            //DataContext = this;
        }
    }
}
