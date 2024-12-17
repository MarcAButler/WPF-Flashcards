using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Flashcards.Models;

namespace WPF_Flashcards.ViewModels
{
    public class ReviewDeckViewModel
    {
        public Deck SelectedDeck { get; set; }
        public Card SelectedCard { get; set; }


        public ReviewDeckViewModel(Deck selectedDeck, Card selectedCard)
        {
            SelectedDeck = selectedDeck;
            SelectedCard = selectedCard;
        }
    }
}
