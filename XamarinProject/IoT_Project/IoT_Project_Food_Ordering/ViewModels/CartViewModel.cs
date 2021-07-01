using IoT_Project_Food_Ordering.DTOs;
using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.Services;
using IoT_Project_Food_Ordering.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<UserCartItem> CartItems { get; set; }

        private decimal _TotalCost;
        public decimal TotalCost
        {
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalCost;
            }
        }

        public Command PlaceOrdersCommand { get; set; }
        public Command ClearCartCommand { get; set; }


        public CartViewModel()
        {
            CartItems = new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
            ClearCartCommand = new Command(async () => await RemoveItemsAndPopBack()); 
        }

        private async Task PlaceOrdersAsync()
        {
            //code to place order
            /*var orderService = new OrderService();
            await orderService.PlaceOrderAsync();*/

            var id = await new OrderService().PlaceOrderAsync() as string;

            if(id.Equals("0"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No items provided for the order.", "Ok");
                return;
            }

            RemoveItemsFromCart();
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersView(id));
        }

        private async Task RemoveItemsAndPopBack()
        {
            CartItemService cartService = new CartItemService();

            cartService.RemoveItemsFromCart();
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void RemoveItemsFromCart()
        {
            CartItemService cartService = new CartItemService();

            cartService.RemoveItemsFromCart();
        }

        private void LoadItems()
        {
            var cn = DependencyService.Get<ISqLite>().GetConnection();
            var items = cn.Table<CartItem>().ToList();
            CartItems.Clear();
            foreach (var item in items)
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Cost = item.Price * item.Quantity
                });
                TotalCost += (item.Price * item.Quantity);
            }
        }
    }
}
