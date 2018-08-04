namespace FastFood.Models
{
    public class OrderItem
    {
        private decimal quantity { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public decimal Quantity
        {
            get => quantity;
            
            set
            {
                if (value <= 0)
                {
                   // TODO: figure out what to do
                }

                quantity = value;
            }
        }
    }
}