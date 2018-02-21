using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var line = Console.ReadLine();
        var people = new List<Person>();

        while (!line.Equals("End"))
        {
            var info = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            PopulatePeopleList(info, people);

            line = Console.ReadLine();
        }

        var query = Console.ReadLine();

        PrintResult(people, query);
    }

    private static void PrintResult(List<Person> people, string query)
    {
        Person desiredPerson = people.Where(p => p.Name == query).First();

        Console.WriteLine(desiredPerson.Name);

        Console.WriteLine("Company:");

        if (desiredPerson.Company != null)
        {
            Console.WriteLine($"{desiredPerson.Company.Name} {desiredPerson.Company.Department} {desiredPerson.Company.Salary:f2}");
        }

        Console.WriteLine("Car:");

        if (desiredPerson.Car != null)
        {
            Console.WriteLine($"{desiredPerson.Car.Model} {desiredPerson.Car.Speed}");
        }

        Console.WriteLine("Pokemon:");
        desiredPerson.Pokemons.ForEach(p => Console.WriteLine(string.Join(" ", p.Name, p.Type)));

        Console.WriteLine("Parents:");
        desiredPerson.Parents.ForEach(p => Console.WriteLine(string.Join(" ", p.Name, p.Birthday)));

        Console.WriteLine("Children:");
        desiredPerson.Children.ForEach(c => Console.WriteLine(string.Join(" ", c.Name, c.Birthday)));
    }

    private static void PopulatePeopleList(string[] info, List<Person> people)
    {
        var personName = info[0];

        // initialize new Person and add it to the people's collection
        if (!people.Any(p => p.Name == personName))
        {
            people.Add(new Person(personName));
        }

        // get person we are looking for
        Person person = people.Where(p => p.Name == personName).First();

        // add data to that person we found in the above step according to parameter at first position in info arr.
        switch (info[1])
        {
            case "company":
                var companyName = info[2];
                var department = info[3];
                var salary = decimal.Parse(info[4]);
                person.Company = new Company(companyName, department, salary);
                break;
            case "pokemon":
                var pokemonName = info[2];
                var pokemonType = info[3];
                person.Pokemons.Add(new Pokemon(pokemonName, pokemonType));
                break;
            case "parents":
                var parentsName = info[2];
                var parentsBDay = info[3];
                person.Parents.Add(new Parent(parentsName, parentsBDay));
                break;
            case "children":
                var childsName = info[2];
                var childsBday = info[3];
                person.Children.Add(new Child(childsName, childsBday));
                break;
            case "car":
                var model = info[2];
                var speed = int.Parse(info[3]);
                person.Car = new Car(model, speed);
                break;
        }
    }
}