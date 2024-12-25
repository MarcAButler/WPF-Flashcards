using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public ICommand ShowAddDeckWindowCommand { get; set; }

        public ICommand ShowDeleteDeckWindowCommand { get; set; }

        public ICommand NavigateToDeckPageCommand { get; set; }
        public ICommand FlipCardCommand { get; set; }
        public ICommand NextCardCommand { get; set; }

        public ICommand SaveDeckCommand { get; set; }

        public ICommand DeleteCardCommand { get; set; }

        public ICommand DeleteDeckCommand { get; set; }
        public ICommand CancelDeleteDeckCommand { get; set; }

        private Deck? _selectedDeck;
        public Deck? SelectedDeck
        {
            get => _selectedDeck;
            set
            {
                _selectedDeck = value;
                OnPropertyChanged();
                CreateCardQueue(_selectedDeck);
                UpdateCurrentSelectedCard();
                if (NavigateToDeckPageCommand.CanExecute(value))
                {
                    NavigateToDeckPageCommand.Execute(value);
                }
            }
        }

        private Card? _selectedCard;
        public Card? SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                OnPropertyChanged();
            }
        }

        public Queue<Card> SelectedDeckCardQueue { get; set; }

        private ReviewState _currentReviewState;
        public ReviewState CurrentReviewState
        {
            get => _currentReviewState;
            set
            {
                _currentReviewState = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested(); // Notify commands to re-evaluate CanExecute
            }
        }

        public MainViewModel()
        {
            Decks = DeckManager.GetDecks();
            SelectedDeckCardQueue = new Queue<Card>();
            ShowAddDeckWindowCommand = new RelayCommand(parameter => ShowWindow("AddDeck"), CanShowWindow);
            ShowDeleteDeckWindowCommand = new RelayCommand(parameter => ShowWindow("DeleteDeck"), CanShowWindow);

            NavigateToDeckPageCommand = new RelayCommand(
                executeMethod: parameter => NavigateToDeckPage(parameter as Deck),
                canExecuteMethod: parameter => parameter is Deck
            );

            FlipCardCommand = new RelayCommand(FlipCard, CanFlipCard);
            NextCardCommand = new RelayCommand(NextCard, CanNextCard);

            CurrentReviewState = ReviewState.Flip;

            SaveDeckCommand = new RelayCommand(SaveDeck, CanSaveDeck);
            DeleteCardCommand = new RelayCommand(DeleteCard, CanDeleteCard);
            DeleteDeckCommand = new RelayCommand(DeleteDeck, CanDeleteDeck);

            CancelDeleteDeckCommand = new RelayCommand(CancelDeleteDeck, CanCancelDeleteDeck);
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object parameter)
        {
            var mainwindow = parameter as Window;
            Window windowToOpen = null;

            if (parameter is string windowType)
            {
                switch (windowType)
                {
                    case "AddDeck":
                        windowToOpen = new AddDeckView
                        {
                            Owner = mainwindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        break;
                    case "DeleteDeck":
                        windowToOpen = new DeleteDeckConfirmationView
                        {
                            Owner = mainwindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            DataContext = this // Set DataContext to MainViewModel
                        };
                        break;
                }
            }

            windowToOpen?.ShowDialog();
        }

        private void NavigateToDeckPage(Deck? selectedDeck)
        {
            if (selectedDeck != null)
            {
                NavigationService.Navigate(new DeckConfirmationPageView(this));
                CurrentReviewState = ReviewState.Flip;
            }
        }

        private void CreateCardQueue(Deck? selectedDeck)
        {
            SelectedDeckCardQueue.Clear();
            if (selectedDeck?.Cards != null)
            {
                foreach (Card card in selectedDeck.Cards)
                {
                    SelectedDeckCardQueue.Enqueue(card);
                }
            }
        }

        public void UpdateCurrentSelectedCard()
        {
            if (SelectedDeckCardQueue.Count > 0)
            {
                SelectedCard = SelectedDeckCardQueue.Dequeue();
                CurrentReviewState = ReviewState.Flip;
            }
            else
            {
                CurrentReviewState = ReviewState.Complete;
            }
        }

        private void FlipCard(object parameter)
        {
            if (CurrentReviewState == ReviewState.Flip)
            {
                CurrentReviewState = ReviewState.Answer;
            }
        }

        private bool CanFlipCard(object parameter)
        {
            return CurrentReviewState == ReviewState.Flip;
        }

        private void NextCard(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("NextCard method called.");
            if (CurrentReviewState == ReviewState.Answer)
            {
                UpdateCurrentSelectedCard();
            }
        }

        private bool CanNextCard(object parameter)
        {
            // For now just always allow going to next card if available
            return true;
        }

        private bool CanSaveDeck(object obj)
        {
            return SelectedDeck != null && SelectedDeck.Cards.Any();
        }

        private void SaveDeck(object obj)
        {
            if (SelectedDeck != null)
            {
                var cardsToRemove = SelectedDeck.Cards.Where(card => card.IsDeleted).ToList();
                foreach (var card in cardsToRemove)
                {
                    SelectedDeck.Cards.Remove(card);
                }

                // Logic to save the deck, e.g., update the database or file
                DeckManager.UpdateDeck(SelectedDeck);

                // Navigate to the confirmation page
                NavigationService.Navigate(new DeckConfirmationPageView(this));
            }
        }

        private bool CanDeleteCard(object obj)
        {
            return obj is Card;
        }

        private void DeleteCard(object obj)
        {
            if (obj is Card card)
            {
                card.IsDeleted = true;
                OnPropertyChanged(nameof(SelectedDeck));
            }
        }

        private bool CanDeleteDeck(object obj)
        {
            return SelectedDeck != null;
        }

        private bool CanCancelDeleteDeck(object obj)
        {
            return true;
        }

        private void DeleteDeck(object obj)
        {
            if (SelectedDeck != null)
            {
                Decks.Remove(SelectedDeck);
                // Save changes to the database or file if necessary
                DeckManager.DeleteDeck(SelectedDeck);
                SelectedDeck = null;

                NavigationService.Navigate(new DeckConfirmationPageView(null));

                CloseChildWindow("deletedeckconfirmationview");
            }
        }

        private void CancelDeleteDeck(object obj)
        {
            CloseChildWindow("deletedeckconfirmationview");
        }


        private void CloseChildWindow(string childDeckName)
        {
            Window addDeckViewWindow = Application.Current.MainWindow;

            foreach (Window win in Application.Current.Windows)
            {
                //if (!win.IsFocused && win.Tag.ToString() == "mdi_child")
                if (win.Name == childDeckName)
                {
                    win.Close();
                }

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
