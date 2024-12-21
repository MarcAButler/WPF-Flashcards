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

        public ICommand ShowWindowCommand { get; set; }
        public ICommand NavigateToDeckPageCommand { get; set; }
        //public ICommand UpdateCardCommand { get; set; }
        public ICommand FlipCardCommand { get; set; }
        public ICommand NextCardCommand { get; set; }

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
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            NavigateToDeckPageCommand = new RelayCommand(
                executeMethod: parameter => NavigateToDeckPage(parameter as Deck),
                canExecuteMethod: parameter => parameter is Deck
            );

            FlipCardCommand = new RelayCommand(FlipCard, CanFlipCard);
            NextCardCommand = new RelayCommand(NextCard, CanNextCard);

            CurrentReviewState = ReviewState.Flip;
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            var mainwindow = obj as Window;
            AddDeckView addDeckWindow = new AddDeckView
            {
                Owner = mainwindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            addDeckWindow.Show();
        }

        private void NavigateToDeckPage(Deck? selectedDeck)
        {
            if (selectedDeck != null)
            {
                NavigationService.Navigate(new DeckEditPageView(this));
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
