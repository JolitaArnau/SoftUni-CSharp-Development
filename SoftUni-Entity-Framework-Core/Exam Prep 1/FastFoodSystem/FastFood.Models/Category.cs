using System.Collections.Generic;

namespace FastFood.Models
{
    public class Category
    {
        private string name { get; set; }

        public Category()
        {
            this.Items = new List<Item>();
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

        public ICollection<Item> Items { get; set; }
    }
}