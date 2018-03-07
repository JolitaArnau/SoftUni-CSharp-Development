using System;
using System.Linq;

public class Cat : Feline
{
    private static readonly string[] FoodTypes = new string[2] {"Vegetable", "Meat"};
    private const double Calories = 0.30;

    public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {
    }

    public override string AskForFood()
    {
        return "Meow";
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
}