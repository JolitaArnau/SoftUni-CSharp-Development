using System;
using System.Collections.Generic;
using FastFood.Models.Enums;
using Type = FastFood.Models.Enums.Type;

namespace FastFood.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        
        public int Id { get; set; }

        public string Customer { get; set; }

        public DateTime DateTime { get; set; }

        public Type Type { get; set; }

        public decimal TotalPrice { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}