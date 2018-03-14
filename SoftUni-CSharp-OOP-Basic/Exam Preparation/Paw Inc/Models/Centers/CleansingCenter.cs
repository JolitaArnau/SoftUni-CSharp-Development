namespace PawInc.Models.Centers
{
    using System.Collections.Generic;
    using System.Linq;
    using Animals;
    
    public class CleansingCenter : Center
    {
        public readonly List<Animal> Animals = new List<Animal>();
        
        public CleansingCenter(string name) : base(name)
        {
        }

        public IEnumerable<Animal> CleanseAnimals()
        {
      
            foreach (var animal in Animals)
            {
                animal.IsCleansed = true;
            }

            return Animals;
        }

        public override string ToString()
        {
            var names = Animals.Where(a => a.IsCleansed).OrderBy(n => n.Name).Select(animal => animal.Name).ToList();

            return $"{string.Join(", ", names)}";
        }
    }
}