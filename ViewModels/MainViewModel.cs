using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Flashcards.Commands;
using WPF_Flashcards.Models;
using WPF_Flashcards.Views;

namespace WPF_Flashcards.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Deck> Decks { get; set; }

        public ICommand ShowWindowCommand { get; set; }


        public MainViewModel() 
        {
            Decks = DeckManager.GetDecks();

            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

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
       
    }
}
