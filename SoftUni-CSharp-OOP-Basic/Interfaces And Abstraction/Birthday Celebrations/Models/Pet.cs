public class Pet : Inhabitant, IBirthable
{
    public Pet(string name, string birthdate) : base(name)
    {
        Birthdate = birthdate;
    }

    public override string Name { get; set; }
    public string Birthdate { get; set; }
}