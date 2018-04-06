using System;


public class Person : IComparable<Person>
{
    public Person(string name, int age, string town)
    {
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }

    private string Name { get; set; }

    private int Age { get; set; }

    private string Town { get; set; }

    public int CompareTo(Person otherPerson)
    {
        var comparisonResult = this.Name.CompareTo(otherPerson.Name);

        if (comparisonResult == 0)
        {
            comparisonResult = this.Age.CompareTo(otherPerson.Age);
        }

        if (comparisonResult == 0)
        {
            comparisonResult = this.Town.CompareTo(otherPerson.Town);
        }

        return comparisonResult;
    }
}