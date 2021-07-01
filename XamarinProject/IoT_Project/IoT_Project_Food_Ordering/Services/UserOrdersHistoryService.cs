using Firebase.Database;
using IoT_Project_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IoT_Project_Food_Ordering.Services
{
    public class UserOrdersHistoryService
    {
        FirebaseClient client;
        List<UserOrdersHistory> UserOrders;
        public UserOrdersHistoryService()
        {
            client = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            UserOrders = new List<UserOrdersHistory>();
        }

        public async Task<List<UserOrdersHistory>> GetOrderDetailsAsync()
        {
            var uname = Preferences.Get("Username", "Guest");

            var orders = (await client.Child("Orders")
                .OnceAsync<Order>())
                .Where(o => o.Object.Username.Equals(uname))
                .Select(o => new Order
                {
                    OrderId = o.Object.OrderId,
                    TotalCost = o.Object.TotalCost,
                }).ToList();

            foreach (var order in orders)
            {
                UserOrdersHistory ordersHistory = new UserOrdersHistory();
                ordersHistory.OrderId = order.OrderId;
                ordersHistory.TotalCost = order.TotalCost;

                var orderDetails = (await client.Child("OrderDetails")
                .OnceAsync<OrderDetails>())
                .Where(o => o.Object.OrderId.Equals(order.OrderId))
                .Select(o => new OrderDetails
                {
                    OrderId = o.Object.OrderId,
                    OrderDetailId = o.Object.OrderDetailId,
                    ProductID = o.Object.ProductID,
                    ProductName = o.Object.ProductName,
                    Quantity = o.Object.Quantity,
                    Price = o.Object.Price,
                    Address = o.Object.Address
                }).ToList();

                ordersHistory.AddRange(orderDetails);

                UserOrders.Add(ordersHistory);
            }

            return UserOrders;
        }
    }
}
