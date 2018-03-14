namespace PawInc.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Models.Animals;
    using Models.Centers;
    
    public class PawIncManager
    {
        private readonly List<AdoptionCenter> adoptionCenters = new List<AdoptionCenter>();
        private readonly List<CleansingCenter> cleansingCenters = new List<CleansingCenter>();

        private AdoptionCenter currentAdoptionCenter;
        
        // register adoption center with provided adoption center name

        public void RegisterAdoptionCenter(string adoptionCenterName)
        {
            var adoptionCenter = new AdoptionCenter(adoptionCenterName);
            adoptionCenters.Add(adoptionCenter);
        }
        
        // register cleansing center with provided adoption center name

        public void RegisterCleansingCenter(string cleansingCenterName)
        {
            var cleansingCenter = new CleansingCenter(cleansingCenterName);
            cleansingCenters.Add(cleansingCenter);
        }
        
        // register a dog at the given adoption center

        public void RegisterDog(List<string> arguments)
        {
            var name = arguments[0];
            var age = int.Parse(arguments[1]);
            var commands = int.Parse(arguments[2]);
            var adoptionCenterName = arguments[3];
            
            var dog = new Dog(name, age, commands) {AdoptionCenterName = adoptionCenterName};
            
            adoptionCenters.FirstOrDefault(n => n.Name.Equals(adoptionCenterName)).AddAnimalUponRegistration(dog);
        }
        
        // register a cat at the given adoption center

        public void RegisterCat(List<string> arguments)
        {
            var name = arguments[0];
            var age = int.Parse(arguments[1]);
            var iq = int.Parse(arguments[2]);
            var adoptionCenterName = arguments[3];
            
            var cat = new Cat(name, age, iq) {AdoptionCenterName = adoptionCenterName};
            
            adoptionCenters.FirstOrDefault(n => n.Name.Equals(adoptionCenterName)).AddAnimalUponRegistration(cat);
        }
        
        // send all uncleansed animals from the given adoption center to the given cleansing center

        public void SendForCleansing(List<string> arguments)
        {
            var adoptionCenterName = arguments[0];
            var cleansingCenterName = arguments[1];

            var adoptionCenter = adoptionCenters.FirstOrDefault(n => n.Name.Equals(adoptionCenterName));
            
            var animalsForCleansing = adoptionCenter
                .AllRegisteredAnimals
                .FindAll(a =>
                a.IsCleansed == false && a.AdoptionCenterName.Equals(adoptionCenterName));

            currentAdoptionCenter = adoptionCenter;

            var cleansingCenter = cleansingCenters
                .FirstOrDefault(n => n.Name.Equals(cleansingCenterName));
            
            cleansingCenter.Animals.AddRange(animalsForCleansing);
        }
        
        // get current adoption center and send cleansed animals back

        public void Cleanse(string cleansingCenterName)
        {
            var cleansingCenter = cleansingCenters.FirstOrDefault(n => n.Name.Equals(cleansingCenterName));

            var cleansedAnimals = cleansingCenter.CleanseAnimals();

            var adoptionCenter = adoptionCenters.FirstOrDefault(n => n.Name.Equals(currentAdoptionCenter.Name));

            adoptionCenter.ReadyForAdoption.AddRange(cleansedAnimals);
            
            adoptionCenter.RemoveCleansedAnimals();
        }
        
        // adopt all cleansed animals from the given adoption center

        public void Adopt(string adoptionCenterName)
        {
            var adoptionCenter = adoptionCenters.FirstOrDefault(a => a.Name.Equals(adoptionCenterName));

            var filteredByAdoptionCenter = adoptionCenter
                .ReadyForAdoption
                .Where(a => a.AdoptionCenterName.Equals(adoptionCenterName))
                .ToList();

            if (adoptionCenter.ReadyForAdoption.Any(a => a.AdoptionCenterName.Equals(adoptionCenterName)))
            {
                adoptionCenter.Adopt(filteredByAdoptionCenter);
            }
        }
        
        // print report

        public string Report()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Paw Incorporative Regular Statistics");
            stringBuilder.AppendLine($"Adoption Centers: {adoptionCenters.Count}");
            stringBuilder.AppendLine($"Cleansing Centers: {cleansingCenters.Count}");
            stringBuilder.Append("Adopted Animals: ");

            foreach (var adoptionCenter in adoptionCenters)
            {
                stringBuilder.Append(adoptionCenter.ToString());
            }

            stringBuilder.AppendLine();

            stringBuilder.Append("Cleansed Animals: ");

            foreach (var cleansingCenter in cleansingCenters)
            {
                stringBuilder.Append(cleansingCenter.ToString());
            }

            stringBuilder.AppendLine();

            var awaitingAdoption = 0;

            foreach (var center in adoptionCenters)
            {
                awaitingAdoption += center.ReadyForAdoption.Count;
            }

            stringBuilder.AppendLine($"Animals Awaiting Adoption: {awaitingAdoption}");

            var awaitingCleansing = 0;

            foreach (var center in cleansingCenters)
            {
                awaitingCleansing += center.Animals.Count(a => a.IsCleansed == false);
            }

            stringBuilder.AppendLine($"Animals Awaiting Cleansing: {awaitingCleansing}");

            return stringBuilder.ToString();
        }
    }
}