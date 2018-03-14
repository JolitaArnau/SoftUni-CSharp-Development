namespace PawInc.Models.Centers
{
    using System.Collections.Generic;
    using System.Linq;
    using Animals;
    
    public class AdoptionCenter : Center
    {
        public readonly List<Animal> AllRegisteredAnimals = new List<Animal>();
        public readonly List<Animal> ReadyForAdoption = new List<Animal>();
        private readonly List<Animal> Adopted = new List<Animal>();

        public AdoptionCenter(string name) : base(name)
        {
        }

        public void AddAnimalUponRegistration(Animal animal)
        {
            AllRegisteredAnimals.Add(animal);
        }

        public void RemoveCleansedAnimals()
        {
            foreach (var animal in ReadyForAdoption)
            {
                AllRegisteredAnimals.Remove(animal);
            }
        }

        public void Adopt(List<Animal> filteredByAdoptionCenter)
        {
            Adopted.AddRange(filteredByAdoptionCenter);

            foreach (var adoptedAnimal in filteredByAdoptionCenter)
            {
                ReadyForAdoption.Remove(adoptedAnimal);
            }
        }

        public override string ToString()
        {
            var names = Adopted.OrderBy(n => n.Name).Select(animal => animal.Name).ToList();

            return $"{string.Join(", ", names)}";
        }
    }
}