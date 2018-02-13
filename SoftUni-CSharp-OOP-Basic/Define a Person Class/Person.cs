public class Person
{
    public string Name;
    public int Age;

    public Person()
    {
        this.Name = "No name";
        this.Age = 1;
    }

    public Person(int age)
        : this()
    {
        this.Age = age;
    }

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }
}