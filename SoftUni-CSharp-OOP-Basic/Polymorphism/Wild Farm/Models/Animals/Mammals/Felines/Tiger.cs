public class Tiger : Feline
{
    private const string FoodType = "Meat";
    private const double Calories = 1.00;

    public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight,
        livingRegion, breed)
    {
    }

    public override string AskForFood()
    {
        return "ROAR!!!";
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