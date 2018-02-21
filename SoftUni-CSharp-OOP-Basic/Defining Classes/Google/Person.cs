using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public Company Company { get; set; }
    public Car Car { get; set; }
    public List<Parent> Parents { get; set; }
    public List<Child> Children { get; set; }
    public List<Pokemon> Pokemons { get; set; }

    public Person(string name)
    {
        Name = name;
        Car = null;
        Parents = new List<Parent>();
        Children = new List<Child>();
        Pokemons = new List<Pokemon>();
    }
}