using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
    public class MainViewModel
    {
        public ObservableCollection<Deck> Decks { get; set; }

        public ICommand ShowWindowCommand { get; set; }
        public ICommand NavigateToDeckPageCommand { get; set; }

        // Handle switching decks
        private Deck? _selectedDeck;
        public Deck? SelectedDeck
        {
            get => _selectedDeck;
            set 
            {
                _selectedDeck = value;
                //OnPropertyChanged();
                NavigateToDeckPageCommand.Execute(value);
            }
        }



        public MainViewModel() 
        {
            Decks = DeckManager.GetDecks();

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
            if (selectedDeck != null)
            {
                NavigationService.Navigate(new DeckEditPageView(selectedDeck));
            }
        }
       
    }
}
