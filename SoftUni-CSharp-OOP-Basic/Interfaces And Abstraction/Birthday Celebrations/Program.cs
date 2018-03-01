using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    public static void Main()
    {
        var line = Console.ReadLine();

        var birthdates = new List<IBirthable>();
        
        while (!line.Equals("End"))
        {
            var inhabitantsTokens = line.Split(' ');
            var typeOfBeing = inhabitantsTokens[0];

            var name = string.Empty;
            var birthdate = string.Empty;
            var id = string.Empty;

            switch (typeOfBeing)
            {
                case "Citizen":
                    name = inhabitantsTokens[1];
                    var age = int.Parse(inhabitantsTokens[2]);
                    id = inhabitantsTokens[3];
                    birthdate = inhabitantsTokens[4];
                    
                    Citizen citizen = new Citizen(name, age, id, birthdate);
                    birthdates.Add(citizen);
                    
                    break;
                case "Pet":
                    name = inhabitantsTokens[1];
                    birthdate = inhabitantsTokens[2];
                    
                    Pet pet = new Pet(name, birthdate);
                    birthdates.Add(pet);
                    
                    break;
            }

            line = Console.ReadLine();
        }

        var queryYear = Console.ReadLine();

        var filteredBirthdates = birthdates.Where(b => b.Birthdate.Contains(queryYear)).ToList();

        foreach (var birthdate in filteredBirthdates)
        {
            Console.WriteLine(birthdate.Birthdate);
        }
    }
}