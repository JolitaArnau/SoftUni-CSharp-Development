public class Hen : Bird
{
    private const double Calories = 0.35;
    
    public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    public override string AskForFood()
    {
        return "Cluck";
    }


    public override bool EatsCertainFoodType(string foodType)
    {
        return true;
    }


    public override double GainWeight(int foodQuantity)
    {
        this.Weight += Calories * foodQuantity;

        return Weight;
    }
}