using System.Collections.Generic;
using PawInc.Models.Animals;

namespace PawInc.Models.Centers
{
    public abstract class Center
    {
        public Center(string name)
        {
            Name = name;
        }
        
        public string Name { get; set; }
    }
}