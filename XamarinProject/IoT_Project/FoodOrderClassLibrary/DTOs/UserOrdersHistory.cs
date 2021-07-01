using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Project_Food_Ordering.Models
{
    public class UserOrdersHistory : List<OrderDetails>
    {
        public string OrderId { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public decimal TotalCost { get; set; }
    }
}
