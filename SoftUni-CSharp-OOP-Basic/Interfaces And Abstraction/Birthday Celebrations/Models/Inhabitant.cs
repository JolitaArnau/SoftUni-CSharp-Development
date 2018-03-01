public abstract class Inhabitant
{
    public Inhabitant(string name)
    {
        this.Name = name;
 
    }
 
    public abstract string Name { get; set; }
}