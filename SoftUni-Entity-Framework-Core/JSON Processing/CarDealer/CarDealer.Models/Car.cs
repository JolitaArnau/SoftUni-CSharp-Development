using System.Numerics;
using Newtonsoft.Json;

namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.PartCars = new HashSet<PartCars>();
            
            this.Sales = new HashSet<Sale>();
        }
        
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }

        [JsonIgnore]
        public virtual ICollection<PartCars> PartCars { get; set; }
    }
}