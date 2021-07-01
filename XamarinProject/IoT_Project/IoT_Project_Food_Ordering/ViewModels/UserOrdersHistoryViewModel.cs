using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.ViewModels
{
    public class UserOrdersHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<UserOrdersHistory> OrderDetails { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }

            get
            {
                return _IsBusy;
            }
        }

        public UserOrdersHistoryViewModel()
        {
            OrderDetails = new ObservableCollection<UserOrdersHistory>();
            LoadUserOrders();
        }

        private async void LoadUserOrders()
        {
            try
            {
                IsBusy = true;
                OrderDetails.Clear();
                var service = new UserOrdersHistoryService();
                var details = await service.GetOrderDetailsAsync();
                foreach (var order in details)
                {
                    OrderDetails.Add(order);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
