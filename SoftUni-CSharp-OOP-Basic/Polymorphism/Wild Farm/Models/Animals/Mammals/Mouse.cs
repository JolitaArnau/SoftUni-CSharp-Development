using System.Linq;

public class Mouse : Mammal
{
    private static readonly string[] FoodTypes = new string[2] {"Vegetable", "Fruit"};
    private const double Calories = 0.10;

    public Mouse(string name, double weight, string livingRegion) : base(name, weight,
        livingRegion)
    {
    }

    public override string AskForFood()
    {
        return "Squeak";
    }

    public override bool EatsCertainFoodType(string foodType)
    {
        return FoodTypes.Any(t => foodType == t);
    }

    public override double GainWeight(int foodQuantity)
    {
        this.Weight += Calories * foodQuantity;

        return Weight;
    }

    public override string ToString()
    {
        return $"{GetType().Name} [{string.Join(", ", Name, Weight, LivingRegion, FoodEaten)}]";
    }
}