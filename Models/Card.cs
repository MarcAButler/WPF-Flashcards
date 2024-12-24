using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Flashcards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string? Front { get; set; }
        public string? Back { get; set; }
        // Used to track soft-deletion of a card before changes are committed
        public bool IsDeleted { get; set; }
    }
}
