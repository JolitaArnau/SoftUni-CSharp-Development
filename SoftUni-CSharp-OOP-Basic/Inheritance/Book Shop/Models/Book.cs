using System;
using System.Text;

public class Book
{
    private string author;
    private string name;
    private decimal price;


    public Book(string author, string name, decimal price)
    {
        Author = author;
        Name = name;
        Price = price;
    }

    public string Name
    {
        get => name;

        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }

            this.name = value;
        }
    }

    public string Author
    {
        get => author;
        set
        {
            var authorName = value.Split(' ');
            if (authorName.Length > 1)
            {
                var lastName = authorName[1];

                if (Char.IsDigit(lastName[0]))
                {
                    throw new ArgumentException("Author not valid!");
                }
            }


            this.author = value;
        }
    }

    public virtual decimal Price
    {
        get => price;

        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }

            this.price = value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Type: ").AppendLine(this.GetType().Name)
            .Append("Title: ").AppendLine(this.Name)
            .Append("Author: ").AppendLine(this.Author)
            .Append("Price: ").Append($"{this.Price:f2}")
            .AppendLine();

        return sb.ToString();
    }
}