public class Animal
{
    public string Name { get; protected set; }
    
    public string FavortiteFood { get; protected set; }

    public Animal(string name, string favoriteFood)
    {
        Name = name;
        FavortiteFood = favoriteFood;
    }

    public virtual string ExplainSelf()
    {
        return $"I am {Name} and my favourite food is {FavortiteFood}";
    }
}