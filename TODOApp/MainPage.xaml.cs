using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TODOApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageViewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var clickedItems = _mainPageViewModel.Items.Where(p => p.IsChecked).ToList();
            var historyViewModel = new HistoryPageViewModel
            {
                HistoryItems = clickedItems
            };

            Navigation.PushAsync(new HistoryPage()
            {
                BindingContext = historyViewModel
            });
        }
    }
}
