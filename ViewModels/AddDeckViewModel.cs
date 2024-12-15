﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WPF_Flashcards.Commands;
using WPF_Flashcards.Models;

namespace WPF_Flashcards.ViewModels
{
    public class AddDeckViewModel : INotifyPropertyChanged
    {
        public ICommand AddDeckCommand { get; set; }
        public ICommand AddCardCommand { get; set; }


        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ObservableCollection<Card> Cards { get; set; }


        // Backing Fields
        private string? _cardFront;
        private string? _cardBack;

        public string? CardFront
        {
            get => _cardFront;
            // Set the public CardFront binding variable to the private backing field
            set
            {
                _cardFront = value;
                OnPropertyChanged(nameof(CardFront));
                (AddCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
        public string? CardBack
        {
            get => _cardBack; 
            set
            {
                _cardBack = value;
                OnPropertyChanged(nameof(CardBack));
                (AddCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }




        public event PropertyChangedEventHandler? PropertyChanged;
  
        public AddDeckViewModel(string deckName = "", string deckDescription = "")
        {
            // Make an empty observable collection of cards for the new deck
            Cards = new ObservableCollection<Card>();

            Name = deckName;

            Description = deckDescription;

            AddDeckCommand = new RelayCommand(AddDeck, CanAddDeck);
            
            AddCardCommand = new RelayCommand(AddCard, CanAddCard);
        }


        private bool CanAddDeck(object obj)
        {
            return true;
        }

        private void AddDeck(object obj)
        {
            DeckManager.AddDeck(new Deck() 
            { 
                Id = 9999,
                Name = Name,
                Description = Description,
                Cards = new List<Card>(Cards)
            }
            );

            CloseAddDeckWindow();
        }


        private bool CanAddCard(object obj)
        {
            return true;
        }

        private void AddCard(object obj)
        {
            System.Diagnostics.Debug.WriteLine("HELLO WORLD");
            System.Diagnostics.Debug.WriteLine("CardFront: " + CardFront);

            Cards.Add(new Card { Front = CardFront, Back = CardBack });
        }

        // Update the View dynamically when the properties change 
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseAddDeckWindow()
        {
            string addDeckViewName = "adddeckview";
            //Window addDeckViewWindow = Application.Current.MainWindow?.FindName(addDeckViewName) as Window;
            Window addDeckViewWindow = Application.Current.MainWindow;

            foreach (Window win in Application.Current.Windows)
            {
                //if (!win.IsFocused && win.Tag.ToString() == "mdi_child")
                if (win.Name == addDeckViewName)
                {
                    win.Close();
                }

            }
        }
    }
}
