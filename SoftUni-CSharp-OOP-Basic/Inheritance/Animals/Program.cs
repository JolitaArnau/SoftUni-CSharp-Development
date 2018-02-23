using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var animals = new List<Animal>();

        var kind = Console.ReadLine();

        while (!kind.Equals("Beast!"))
        {
            var animalInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var name = animalInfo[0];
            var age = int.Parse(animalInfo[1]);
            var gender = animalInfo[2];

            try
            {
                Animal animal = AnimalFactory.GetAnimal(kind, name, age, gender);
                animals.Add(animal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            kind = Console.ReadLine();
        }

        Console.WriteLine(string.Join(Environment.NewLine, animals));
    }
}