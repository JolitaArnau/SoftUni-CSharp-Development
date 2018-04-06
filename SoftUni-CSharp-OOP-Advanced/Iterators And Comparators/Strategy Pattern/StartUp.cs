using System;
using System.Collections.Generic;

public class StartUp
{
    public static void Main()
    {
        var peopleCount = int.Parse(Console.ReadLine());

        var peopleSortedByName = new SortedSet<Person>(new NameComparer());
        var peopleSortedByAge = new SortedSet<Person>(new AgeComparer());

        for (int i = 0; i < peopleCount; i++)
        {
            var personData = Console.ReadLine().Split();
            var name = personData[0];
            var age = int.Parse(personData[1]);

            var person = new Person(name, age);

            peopleSortedByName.Add(person);
            peopleSortedByAge.Add(person);
        }

        Console.WriteLine(string.Join(Environment.NewLine, peopleSortedByName));
        Console.WriteLine(string.Join(Environment.NewLine, peopleSortedByAge));
    }
}