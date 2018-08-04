using System.Collections.Generic;

namespace FastFood.Models
{
    public class Position
    {
        private string name { get; set; }

        public Position()
        {
            this.Employees = new List<Employee>();
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

        public ICollection<Employee> Employees { get; set; }
    }
}