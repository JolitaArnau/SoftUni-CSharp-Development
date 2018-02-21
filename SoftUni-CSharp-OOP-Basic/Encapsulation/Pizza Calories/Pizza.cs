using System;
using System.Collections.Generic;
using System.Linq;

public class Pizza
{
    private const string PizzaNameLength = "Pizza name should be between 1 and 15 symbols.";
    private const int MinToppingsCount = 0;
    private const int MaxToppingsCount = 10;

    private string name;
    private int numberOfToppings;
    private Dough dough;
    private List<Topping> toppings;

    public Pizza(string name)
    {
        this.Name = name;
        this.Dough = dough;
        this.toppings = new List<Topping>();
    }


    public string Name
    {
        get => this.name;

        set
        {
            if (value.Length > 15 || string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(PizzaNameLength);
            }

            this.name = value;
        }
    }


    public Dough Dough
    {
        set => this.dough = value;
    }


    public void AddTopping(Topping topping)
    {
        this.toppings.Add(topping);
    }

    public double CalculateTotalPizzaCalories()
    {
        return this.dough.CalculateDoughCalories() + this.toppings.Sum(t => t.CalculateToppingCalories());
    }
}