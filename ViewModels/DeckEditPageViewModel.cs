//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows;
//using System.Windows.Input;
//using WPF_Flashcards.Commands;
//using WPF_Flashcards.Models;
//using WPF_Flashcards.Views;

//public class DeckEditPageViewModel : INotifyPropertyChanged
//{
//    private Deck _selectedDeck;
//    public Deck SelectedDeck
//    {
//        get => _selectedDeck;
//        set
//        {
//            _selectedDeck = value;
//            OnPropertyChanged(nameof(SelectedDeck));
//        }
//    }

//    public ICommand DeleteDeckCommand { get; }
//    public ICommand ShowDeleteDeckWindowCommand { get; }

//    public DeckEditPageViewModel()
//    {
//        DeleteDeckCommand = new RelayCommand(param => DeleteDeck(), param => CanDeleteDeck());
//        ShowDeleteDeckWindowCommand = new RelayCommand(param => ShowDeleteDeckWindow());
//        ShowDeleteDeckWindowCommand = new RelayCommand(param => ShowDeleteDeckWindow(), param => true);
//    }

//    private void DeleteDeck()
//    {
//        if (SelectedDeck != null)
//        {
//            // Logic to delete the deck
//            // For example, remove it from a collection and save changes
//            // Assuming you have a collection of decks in your ViewModel
//            Decks.Remove(SelectedDeck);
//            // Save changes to the database or file if necessary
//        }
//    }

//    private bool CanDeleteDeck()
//    {
//        return SelectedDeck != null;
//    }

//    private void ShowDeleteDeckWindow()
//    {
//        var deleteDeckWindow = new DeleteDeckConfirmationView
//        {
//            DataContext = this // Pass the current ViewModel as DataContext
//        };
//        deleteDeckWindow.ShowDialog();
//    }

//    // Assuming you have a collection of decks in your ViewModel
//    private ObservableCollection<Deck> _decks;
//    public ObservableCollection<Deck> Decks
//    {
//        get => _decks;
//        set
//        {
//            _decks = value;
//            OnPropertyChanged(nameof(Decks));
//        }
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;

//    protected virtual void OnPropertyChanged(string propertyName)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}
