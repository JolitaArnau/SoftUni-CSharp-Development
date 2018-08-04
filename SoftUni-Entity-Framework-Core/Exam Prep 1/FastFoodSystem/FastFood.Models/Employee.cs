using System.Collections.Generic;

namespace FastFood.Models
{
    public class Employee
    {
        private string name { get; set; }

        private int age { get; set; }

        public Employee()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Name
        {
            get => name;

            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                }

                name = value;
            }
        }

        public int Age
        {
            get => age;

            set
            {
                if (age < 15 || age > 80)
                {
                    // TODO: figure out what to do
                }

                age = value;
            }
        }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}