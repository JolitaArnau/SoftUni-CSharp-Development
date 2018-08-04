using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Item
    {
        private decimal price { get; set; }

        private string name { get; set; }
        
        public Item()
        {
            this.OrderItems = new List<OrderItem>();
        }
        
        public int Id { get; set; }

        public string Name
        {
            get => name;

            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    // TODO: figure out what to do
                }

                name = value;
            }
        }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price
        {
            get => price;

            set
            {
                if (value < 0 || value < 0.01m)
                {
                    // TODO: figure out what to do
                }

                price = value;
            }
        }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}