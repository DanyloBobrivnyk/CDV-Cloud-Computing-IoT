using IoT_Project_Food_Ordering.Helpers;
using IoT_Project_Food_Ordering.Models;
using Lab1_bobrivnyk.Rest.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.Services
{
    public class CartItemService
    {
        public int GetUserCartCount()
        {
            var cn = DependencyService.Get<ISqLite>().GetConnection();
            var count = cn.Table<CartItem>().Count();
            cn.Close();
            return count;
        }

        public string GetUserJwtToken()
        {
            var cn = DependencyService.Get<ISqLite>().GetConnection();
            var res = cn.Table<JwtToken>().FirstOrDefault().ToString();
            cn.Close();
            return res;
        }
        public void RemoveItemsFromCart()
        {
            var cn = DependencyService.Get<ISqLite>().GetConnection();
            cn.DeleteAll<CartItem>();
            cn.Commit();
            cn.Close();
        }
    }
}
