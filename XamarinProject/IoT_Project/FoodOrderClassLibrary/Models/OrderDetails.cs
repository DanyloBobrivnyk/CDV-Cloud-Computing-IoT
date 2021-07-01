using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Project_Food_Ordering.Models
{
    public class OrderDetails
    {
        public string OrderDetailsId { get; set; }
        public string OrderId { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Address { get; set; }
    }
}
