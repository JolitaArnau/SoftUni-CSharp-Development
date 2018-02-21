using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var line = Console.ReadLine();

        var pizzaLine = line.Split();
        var pizzaName = pizzaLine[1];

        var doughInfo = Console.ReadLine().Split();

        Pizza pizza;

        try
        {
            var doughType = doughInfo[1];
            var technique = doughInfo[2];
            var doughWeight = int.Parse(doughInfo[3]);

            Dough dough = new Dough(doughType, technique, doughWeight);

            pizza = new Pizza(pizzaName);

            pizza.Dough = dough;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }


        line = Console.ReadLine();

        var counter = 0;

        Topping topping;

        while (!line.Equals("END"))
        {
            if (counter <= 10)
            {

                try
                {
                    var toppingInfo = line.Split();
                    var toppingType = toppingInfo[1];
                    var toppingWeight = int.Parse(toppingInfo[2]);

                    topping = new Topping(toppingType, toppingWeight);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                pizza.AddTopping(topping);
            }
            else
            {
                Console.WriteLine("Number of toppings should be in range [0..10].");
                return;
            }

            counter++;

            line = Console.ReadLine();
        }

        Console.WriteLine($"{pizza.Name} - {pizza.CalculateTotalPizzaCalories():f2} Calories.");
    }
}