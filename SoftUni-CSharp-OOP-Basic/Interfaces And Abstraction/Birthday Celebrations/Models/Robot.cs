public class Robot : Inhabitant, IIdentifiable
{
    public Robot(string name, string id) : base(name)
    {
        Id = id;
    }
 
    public override string Name { get; set; }
    public string Id { get; set; }
}