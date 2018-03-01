using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    public static void Main()
    {
        var inhabitantsCount = int.Parse(Console.ReadLine());
        var name = string.Empty;
        var age = 0;

        var buyers = new HashSet<IBuyer>();

        for (int i = 0; i < inhabitantsCount; i++)
        {
            var inhabitantInfo = Console.ReadLine().Split(' ');

            if (inhabitantInfo.Length == 4)
            {
                name = inhabitantInfo[0];
                age = int.Parse(inhabitantInfo[1]);
                var id = inhabitantInfo[2];
                var birthdate = inhabitantInfo[3];

                Citizen citizen = new Citizen(name, age, id, birthdate);
                citizen.BuyFood();
                buyers.Add(citizen);
            }
            else
            {
                name = inhabitantInfo[0];
                age = int.Parse(inhabitantInfo[1]);
                var group = inhabitantInfo[2];

                Rebel rebel = new Rebel(name, age, group);
                rebel.BuyFood();
                buyers.Add(rebel);
            }
        }

        var totalAmount = 0;

        var line = Console.ReadLine();

        while (!line.Equals("End"))
        {
            var currentBuyerName = line;

            var personExists = buyers.Any(b => b.Name.Equals(currentBuyerName));

            if (personExists)
            {
                var buyer = buyers.FirstOrDefault(b => b.Name.Equals(currentBuyerName));
                totalAmount += buyer.Food;
            }

            line = Console.ReadLine();
        }

        Console.WriteLine(totalAmount);
    }
}