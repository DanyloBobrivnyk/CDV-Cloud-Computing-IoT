using IoT_Project_Food_Ordering.DTOs;
using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.Services;
using IoT_Project_Food_Ordering.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            get { return _UserCartItemsCount; }
            set { _UserCartItemsCount = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<FoodItem> LatestItems { get; set; }
        public ObservableCollection<FoodItemWithCategoryImage> LatestItemsWithCategories { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; } 
        public Command ViewOrderHistoryCommand { get; set; }

        public ProductsViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
                UserName = "Guest";
            else
                UserName = uname;

            UserCartItemsCount = new CartItemService().GetUserCartCount();

            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<FoodItem>();
            LatestItemsWithCategories = new ObservableCollection<FoodItemWithCategoryImage>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            ViewOrderHistoryCommand = new Command(async () => await ViewOrderHistoryAsync());
            /*ViewOrdersHistoryCommand = new Command(async () => await ViewOrderHistoryAsync());
            SearchViewCommand = new Command(async () => await SearchViewAsync());*/

            GetCategories();
            GetLatestItems();
            GetLatestItemsWithCategoryImage();
        }

        private async void GetLatestItemsWithCategoryImage()
        {
            var data = await new FoodItemService().GetLatestFoodItemsWithCategoryAsync();
            LatestItemsWithCategories.Clear();
            foreach (var item in data)
            {
                LatestItemsWithCategories.Add(item);
            }
        }

        private async Task ViewOrderHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrderHistoryView());
        }

        private async Task ViewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }
        private async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);
            }
        }

        private async void GetLatestItems()
        {
            var data = await new FoodItemService().GetLatestFoodItemsAsync();
            LatestItems.Clear();
            foreach (var item in data)
            {
                LatestItems.Add(item);
            }
        }
    }
}
