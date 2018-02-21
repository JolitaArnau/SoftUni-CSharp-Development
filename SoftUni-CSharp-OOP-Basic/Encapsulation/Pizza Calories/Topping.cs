using System;

public class Topping
{
    private const int BaseCaloriesPerGram = 2;
    private const int MinToppingWeight = 1;
    private const int MaxToppingWeight = 50;

    private string type;
    private int weight;

    public Topping(string type, int weight)
    {
        this.Type = type;
        this.Weight = weight;
    }

    public string Type
    {
        get => this.type;

        private set
        {
            if (!(value.ToLower().Equals("meat") || value.ToLower().Equals("veggies") ||
                  value.ToLower().Equals("cheese") || value.ToLower().Equals("sauce")))
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }

            this.type = value;
        }
    }

    public int Weight
    {
        get { return this.weight; }

        private set
        {
            if (value < MinToppingWeight || value > MaxToppingWeight)
            {
                throw new ArgumentException(
                    $"{this.type} weight should be in the range [{MinToppingWeight}..{MaxToppingWeight}].");
            }

            this.weight = value;
        }
    }

    public double CalculateToppingCalories()
    {
        double modifier = BaseCaloriesPerGram;
        switch (this.Type.ToLower())
        {
            case "meat":
                modifier *= 1.2;
                break;
            case "veggies":
                modifier *= 0.8;
                break;
            case "cheese":
                modifier *= 1.1;
                break;
            case "sauce":
                modifier *= 0.9;
                break;
        }

        return modifier * this.Weight;
    }
}