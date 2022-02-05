using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace TODOApp
{
    public class Item : BaseNotify
    {
        private bool _isChecked;
        private TextDecorations _textDecoration;

        public string Description { get; set; }
        public DateTime SelectedDate { get; set; }

        public bool IsOverdue => SelectedDate.Date < DateTime.Now && !IsChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;

                _textDecoration = _isChecked ? TextDecorations.Strikethrough
                    : TextDecorations.None;

                OnPropertyChanged(nameof(IsChecked));
                OnPropertyChanged(nameof(TextDecoration));
                OnPropertyChanged(nameof(IsOverdue));
            }
        }

        public TextDecorations TextDecoration
        {
            get => _textDecoration;
            set
            {
                _textDecoration = value;
                OnPropertyChanged(nameof(TextDecoration));
            } 
        }
    }

    public class HistoryPageViewModel : BaseNotify
    {
        private List<Item> _historyItems;

        public List<Item> HistoryItems
        {
            get => _historyItems;
            set
            {
                _historyItems = value;
            }
        }

        public HistoryPageViewModel()
        {
        }
    }

    public class MainPageViewModel : BaseNotify
    {
        public ICommand OnCreateItemCommand { get; }
        public ICommand OnDeleteItemCommand { get; }

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                OnPropertyChanged(nameof(Items));
            }
        }

        public MainPageViewModel()
        {
            OnCreateItemCommand = new Command(OnCreateButtonClick);
            OnDeleteItemCommand = new Command(OnDeleteItem);
        }

        private void OnDeleteItem(object obj)
        {
            if(obj is Item item)
            {
                Items.Remove(item);
                OnPropertyChanged(nameof(Items));
            }
        }

        private void OnCreateButtonClick(object obj)
        {
            var item = new Item
            {
                Description = Description,
                SelectedDate = SelectedDate
            };
            Items.Add(item);
        }
    }
}
