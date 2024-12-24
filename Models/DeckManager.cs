using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Flashcards.Models
{
    // Used to represent a mock database
    public class DeckManager
    {
        private static ObservableCollection<Deck> _DecksDatabase = new ObservableCollection<Deck>()
        {
            new Deck
            {
                Id = 1,
                Name = "Math Flashcards",
                Description = "Basic math operations flashcards.",
                Cards = new ObservableCollection<Card>
                {
                    new Card { Id = 1, Front = "2 + 2", Back = "4" },
                    new Card { Id = 2, Front = "3 x 3", Back = "9" },
                    new Card { Id = 3, Front = "10 / 2", Back = "5" }
                }
            },
            new Deck
            {
                Id = 2,
                Name = "Science Flashcards",
                Description = "Basic science facts.",
                Cards = new ObservableCollection<Card>
                {
                    new Card { Id = 1, Front = "Water's chemical formula", Back = "H2O" },
                    new Card { Id = 2, Front = "Speed of light (in m/s)", Back = "3x10^8" },
                    new Card { Id = 3, Front = "Gravity on Earth (m/s^2)", Back = "9.8" }
                }
            }
        };

        public static ObservableCollection<Deck> GetDecks()
        {
            return _DecksDatabase;
        }

        public static void AddDeck(Deck deck)
        {
            _DecksDatabase.Add(deck);
        }

        internal static void UpdateDeck(Deck? deck)
        {
            var existingDeck = GetDecks().FirstOrDefault(d => d.Id == deck.Id);
            if (existingDeck != null)
            {
                existingDeck.Name = deck.Name;
                existingDeck.Description = deck.Description;
                existingDeck.Cards = new ObservableCollection<Card>(deck.Cards);
            }
        }
    }
}
