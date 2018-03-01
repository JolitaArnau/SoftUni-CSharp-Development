public class Citizen : Inhabitant, IAge, IIdentifiable, IBirthable
{
    public Citizen(string name, int age, string id, string birthdate) : base(name)
    {
        Age = age;
        Id = id;
        Birthdate = birthdate;
    }

    public override string Name { get; set; }
    public int Age { get; set; }
    public string Id { get; set; }
    public string Birthdate { get; set; }
    
}