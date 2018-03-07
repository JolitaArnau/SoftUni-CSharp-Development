public class Owl : Bird
{
    private const string FoodType = "Meat";
    private const double Calories = 0.25;
    
    public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    public override string AskForFood()
    {
        return "Hoot Hoot";
    }

    public override bool EatsCertainFoodType(string foodType)
    {
        return foodType == FoodType;
    }

    public override double GainWeight(int foodQuantity)
    {
        this.Weight += Calories * foodQuantity;

        return Weight;
    }
}