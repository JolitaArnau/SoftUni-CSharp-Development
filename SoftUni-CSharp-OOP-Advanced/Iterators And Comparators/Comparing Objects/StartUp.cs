using System;
using System.Collections.Generic;


public class StartUp
{
    public static void Main()
    {
        var people = new List<Person>();

        var line = Console.ReadLine();
        while (!line.Equals("END"))
        {
            var args = line.Split();
            var name = args[0];
            var age = int.Parse(args[1]);
            var town = args[2];
            var person = new Person(name, age, town);
            people.Add(person);

            line = Console.ReadLine();
        }

        var personIndex = int.Parse(Console.ReadLine());
        
        var equalPeopleCountToWantedPerson = GetPeopleCountEqualToWantedPerson(people[personIndex - 1], people);
        var notEqualPeopleCountToWantedPerson = GetPeopleCountNotEqualToWantedPerson(people[personIndex - 1], people);
        
        
        // if there count of people who equal the wanted person is zero or one (person equals himself) => there is no match
        if (equalPeopleCountToWantedPerson == 0 || equalPeopleCountToWantedPerson == 1)
        {
            Console.WriteLine("No matches");
            return;
        }

        Console.WriteLine($"{equalPeopleCountToWantedPerson} {notEqualPeopleCountToWantedPerson} {people.Count}");

    }
    
    private static int GetPeopleCountEqualToWantedPerson(Person person, List<Person> people)
    {
        var counter = 0;
        foreach (var currentPerson in people)
        {
            if (person.CompareTo(currentPerson) == 0)
            {
                counter++;
            }
        }

        return counter;
    }
    
    private static int GetPeopleCountNotEqualToWantedPerson(Person person, List<Person> people)
    {
        var counter = 0;
        foreach (var currentPerson in people)
        {
            if (person.CompareTo(currentPerson) != 0)
            {
                counter++;
            }
        }

        return counter;
    }
}