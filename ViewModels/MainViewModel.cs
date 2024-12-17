using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Flashcards.Commands;
using WPF_Flashcards.Models;
using WPF_Flashcards.Services;
using WPF_Flashcards.Views;

namespace WPF_Flashcards.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Deck> Decks { get; set; }

        public ICommand ShowWindowCommand { get; set; }
        public ICommand NavigateToDeckPageCommand { get; set; }

        // Handle switching decks and cards
        private Deck? _selectedDeck;
        public Deck? SelectedDeck
        {
            get => _selectedDeck;
            set 
            {
                _selectedDeck = value;
                //OnPropertyChanged();
                NavigateToDeckPageCommand.Execute(value);

                // Whenever a deck is selected create a queue for SelectedDeckCardQueue
                CreateCardQueue(_selectedDeck);

                // For the first time that a deck queue is created also update the SelectedCard
                UpdateCurrentSelectedCard();
            }
        }



        private Card? _selectedCard;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Card? SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                //OnPropertyChanged();
                //NavigateToDeckPageCommand.Execute(value);
            }
        }

        public Queue<Card> SelectedDeckCardQueue { get; set; }





        public MainViewModel() 
        {
            Decks = DeckManager.GetDecks();

            SelectedDeckCardQueue = new Queue<Card>();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            //NavigateToDeckPageCommand = new RelayCommand<Deck>(NavigateToDeckPage);
            NavigateToDeckPageCommand = new RelayCommand(
                executeMethod: parameter => NavigateToDeckPage(parameter as Deck),
                canExecuteMethod: parameter => parameter is Deck
            );
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        // Open the Add Deck Window
        private void ShowWindow(object obj)
        {
            var mainwindow = obj as Window;

            AddDeckView addDeckWindow = new AddDeckView();
            
            // Set the parent and child
            addDeckWindow.Owner = mainwindow;
            // Make children start in the center of the parent window
            addDeckWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addDeckWindow.Show();

        }

        private void NavigateToDeckPage(Deck? selectedDeck)
        {
            // Update Selected Card

            if (selectedDeck != null)
            {
                NavigationService.Navigate(new DeckEditPageView(selectedDeck));
            }
        }

        private void NavigateToReviewDeckPage(Deck? selectedDeck)
        {
            if (selectedDeck != null)
            {
                NavigationService.Navigate(new ReviewDeckPageView(selectedDeck));
            }
        }


        private void CreateCardQueue(Deck? selectedDeck)
        {
            List<Card> selectedDeckCards = selectedDeck.Cards;
            selectedDeckCards = selectedDeck.Cards;
            

            // Add all the cards to the queue
            foreach (Card card in selectedDeckCards)
            {
                SelectedDeckCardQueue.Enqueue(card);
            }

        }

        // Updates the current selectedcard using the SelectedDeckCardQueue
        public void UpdateCurrentSelectedCard()
        {
            SelectedCard = SelectedDeckCardQueue.Dequeue();
        }

    }
}
