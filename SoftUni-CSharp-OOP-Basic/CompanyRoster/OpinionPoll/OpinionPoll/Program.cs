using System;
using System.Collections.Generic;
using System.Linq;
using OpinionPoll;

class Program
{
    static void Main()
    {
        int peopleCount = int.Parse(Console.ReadLine());
        var people = new List<Person>();

        for (int i = 0; i < peopleCount; i++)
        {
            var peopleInfo = Console.ReadLine().Split(' ').ToArray();
            
            people.Add(new Person(peopleInfo[0], int.Parse(peopleInfo[1])));
        }

        var filtered = people.Where(p => p.Age > 30).OrderBy(n => n.Name).ToList();

        foreach (var person in filtered)
        {
            Console.WriteLine(person.Name + " - " + person.Age);
        }
    }
}