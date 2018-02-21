using System;
using System.Collections.Generic;

public class Person
{
    private string name;
    private int money;
    private List<Product> shoppingBag;

    public Person(string name, int money)
    {
        Name = name;
        Money = money;
        this.shoppingBag = new List<Product>();
    }

    public string Name
    {
        get { return this.name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException("Name cannot be empty");
            }

            this.name = value;
        }
    }

    public int Money
    {
        get { return this.money; }
        set 
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }

            this.money = value;
        }
    }
    
    public void AddProductToBag(Product product)
    {
        this.shoppingBag.Add(product);
    }

    public List<Product> ShowShoppingBag()
    {
        return this.shoppingBag;
    }

}