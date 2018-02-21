using System;

public class Product
{
    private string name;
    private int price;

    public Product(string name, int price)
    {
        this.Name = name;
        this.Price = price;
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

    public int Price
    {
        get { return this.price; }
        private set 
        {
            this.price = value;
        }
    }
}