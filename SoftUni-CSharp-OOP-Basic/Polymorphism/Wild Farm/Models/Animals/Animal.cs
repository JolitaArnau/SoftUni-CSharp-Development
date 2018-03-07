using System;

public abstract class Animal : IEat, IFeedable
{
    public Animal(string name, double weight)
    {
        Name = name;
        Weight = weight;
        FoodEaten = 0;
    }

    public string Name { get; set; }

    public double Weight { get; set; }

    public int FoodEaten { get; set; }

    public virtual string AskForFood()
    {
        return string.Empty;
    }

    public virtual bool EatsCertainFoodType(string foodType)
    {
        return false;
    }

    public virtual double GainWeight(int foodQuantity)
    {
        return 0.0;
    }
}