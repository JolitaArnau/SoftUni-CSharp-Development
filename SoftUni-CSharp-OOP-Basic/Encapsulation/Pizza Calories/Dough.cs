using System;

public class Dough
{
    private const string InvalidDoughType = "Invalid type of dough.";
    private const string InvalidDoughWeight = "Dough weight should be in the range [1..200].";
    private const int MinDoughWeight = 1;
    private const int MaxDoughWeight = 200;
    private const int BaseCaloriesPerGram = 2;

    private string flour;
    private string bakingTechnique;
    private int weight;

    public Dough(string flour, string bakingTechnique, int weight)
    {
        this.Flour = flour;
        this.BakingTechnique = bakingTechnique;
        this.Weight = weight;
    }

    public string Flour
    {
        get { return this.flour; }

        private set
        {
            if (!(value.ToLower().Equals("white") || value.ToLower().Equals("wholegrain")))
            {
                throw new ArgumentException(InvalidDoughType);
            }

            this.flour = value;
        }
    }

    public string BakingTechnique
    {
        get { return this.bakingTechnique; }

        private set
        {
            if (!(value.ToLower().Equals("crispy") || value.ToLower().Equals("chewy") ||
                  value.ToLower().Equals("homemade")))
            {
                throw new ArgumentException(InvalidDoughType);
            }

            this.bakingTechnique = value;
        }
    }

    public int Weight
    {
        get { return this.weight; }

        private set
        {
            if (value < MinDoughWeight || value > MaxDoughWeight)
            {
                throw new ArgumentException(InvalidDoughWeight);
            }

            this.weight = value;
        }
    }

    public double CalculateDoughCalories()
    {
        double modifier = BaseCaloriesPerGram;

        switch (this.Flour.ToLower())
        {
            case "white":
                modifier *= 1.5;
                break;
            case "wholegrain":
                modifier *= 1.0;
                break;
        }

        switch (this.BakingTechnique.ToLower())
        {
            case "crispy":
                modifier *= 0.9;
                break;
            case "chewy":
                modifier *= 1.1;
                break;
            case "homemade":
                modifier *= 1.0;
                break;
        }

        return modifier * this.Weight;
    }
}