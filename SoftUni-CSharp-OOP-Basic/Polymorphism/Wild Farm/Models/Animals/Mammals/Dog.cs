public class Dog : Mammal
{
    private const string FoodType = "Meat";
    private const double Calories = 0.40;

    public Dog(string name, double weight, string livingRegion) : base(name, weight,
        livingRegion)
    {
    }

    public override string AskForFood()
    {
        return "Woof!";
    }

    public  override bool EatsCertainFoodType(string foodType)
    {
        return foodType == FoodType;
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