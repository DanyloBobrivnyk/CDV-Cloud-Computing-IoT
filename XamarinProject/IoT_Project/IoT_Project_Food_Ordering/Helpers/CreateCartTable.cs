using IoT_Project_Food_Ordering.Models;
using Lab1_bobrivnyk.Rest.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.Helpers
{
    public class CreateCartTable
    {
        public bool CreateTable()
        {
            try
            {
                var cn = DependencyService.Get<ISqLite>().GetConnection();
                cn.CreateTable<CartItem>();
                cn.CreateTable<JwtToken>();
                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
