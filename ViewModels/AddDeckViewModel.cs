using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Flashcards.Commands;
using WPF_Flashcards.Models;

namespace WPF_Flashcards.ViewModels
{
    public class AddDeckViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Card>? Cards { get; set; }
    

        public ICommand AddDeckCommand { get; set; }

        public AddDeckViewModel()
        {
            AddDeckCommand = new RelayCommand(AddDeck, CanAddDeck);
        }

        private bool CanAddDeck(object obj)
        {
            return true;
        }

        private void AddDeck(object obj)
        {
            DeckManager.AddDeck(new Deck() { Id = 9999, Name = Name, Description = Description, Cards = Cards });
        }
    }
}
