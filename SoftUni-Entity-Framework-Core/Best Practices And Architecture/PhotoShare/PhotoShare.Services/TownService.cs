namespace PhotoShare.Services
{
   using System;
   using System.Linq;
    
    using Models;
    using Contracts;
    using Data;
    
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }
        
        public Town Create(string townName, string countryName)
        {
            if (this.ByName(townName) != null)
            {
                throw new ArgumentException("Town already exists");
            }
            
            var town = new Town()
            {
                Name = townName,
                Country = countryName
               
            };

            context.Towns.Add(town);

            context.SaveChanges();

            return town;
        }

        public Town ByName(string townName)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name.Equals(townName));

            return town;
        }
    }
}