using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    public static void Main()
    {
        var personInfo = Console.ReadLine();

        var people = new List<Person>();

        CreatePerson(personInfo, people);

        var productInfo = Console.ReadLine();

        var products = new List<Product>();

        CreateProduct(productInfo, products);

        var command = Console.ReadLine();

        while (!command.Equals("END"))
        {
            var shoppingTokens = command.Split(' ');
            var personName = shoppingTokens[0];
            var product = shoppingTokens[1];

            ProcessShoppingRequests(people, personName, products, product);

            command = Console.ReadLine();
        }

        PrintResult(people);
    }

    private static void PrintResult(List<Person> people)
    {
        foreach (var person in people)
        {
            if (person.ShowShoppingBag().Any())
            {
                Console.WriteLine($"{person.Name} - {string.Join(", ", person.ShowShoppingBag().Select(p => p.Name))}");
            }
            else
            {
                Console.WriteLine($"{person.Name} - Nothing bought");
            }
        }
    }

    private static void ProcessShoppingRequests(List<Person> people, string personName, List<Product> products,
        string product)
    {
        var desiredPerson = people.FirstOrDefault(p => p.Name.Equals(personName));
        var desiredProduct = products.FirstOrDefault(p => p.Name.Equals(product));

        if (desiredPerson == null || desiredProduct == null)
        {
            return;
        }

        if (desiredPerson.Money >= desiredProduct.Price)
        {
            desiredPerson.AddProductToBag(desiredProduct);
            desiredPerson.Money -= desiredProduct.Price;

            Console.WriteLine($"{desiredPerson.Name} bought {desiredProduct.Name}");
        }
        else
        {
            Console.WriteLine($"{desiredPerson.Name} can't afford {desiredProduct.Name}");
        }
    }

    private static void CreateProduct(string productInfo, List<Product> products)
    {
        var tokens = productInfo.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            var info = token.Split('=');
            var name = info[0].Trim();
            var price = int.Parse(info[1].Trim());
            try
            {
                products.Add(new Product(name, price));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }

    private static void CreatePerson(string personInfo, List<Person> people)
    {
        var tokens = personInfo.Split(';', StringSplitOptions.RemoveEmptyEntries);
        foreach (var token in tokens)
        {
            var info = token.Split('=');
            var name = info[0].Trim();
            var money = int.Parse(info[1].Trim());
            try
            {
                people.Add(new Person(name, money));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}