using Firebase.Database;
using Firebase.Database.Query;
using IoT_Project_Food_Ordering.Helpers;
using IoT_Project_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IoT_Project_Food_Ordering.Services
{
    public class OrderService
    {
        FirebaseClient client;
        Geocoder _geocoder;
        public OrderService()
        {
            client = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            _geocoder = new Geocoder();
        }

        public async Task<string> PlaceOrderAsync()
        {
            var cn = DependencyService.Get<ISqLite>().GetConnection();
            var data = cn.Table<CartItem>().ToList();

            if(data.Count == 0)
            {
                return "0";
            }
                
            var orderId = Guid.NewGuid().ToString();
            var uname = Preferences.Get("Username", "Guest");

            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromMilliseconds(1000)));
            Position pos = new Position(result.Latitude, result.Longitude);
            
            var addresses = await _geocoder.GetAddressesForPositionAsync(pos);

            var address = addresses.FirstOrDefault()?.ToString();

            decimal totalCost = 0;

            foreach (var item in data)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderId = orderId,
                    OrderDetailId = Guid.NewGuid().ToString(),
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Lat = result.Latitude,
                    Lon = result.Longitude,
                    Address = address
                };
                totalCost += item.Price * item.Quantity;

                await client.Child("OrderDetails").PostAsync(orderDetails);
            }

            await client.Child("Orders").PostAsync(
                new Order()
                {
                    OrderId = orderId,
                    Username = uname,
                    TotalCost = totalCost
                }
            );

            return orderId;
        }

        private void RemoveItemsFromCart()
        {
            var cis = new CartItemService();
            cis.RemoveItemsFromCart();
        }
    }
}
