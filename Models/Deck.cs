using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Flashcards.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();
    }
}

