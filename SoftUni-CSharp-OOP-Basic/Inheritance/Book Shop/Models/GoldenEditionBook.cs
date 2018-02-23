using System;

public class GoldenEditionBook : Book
{
    public GoldenEditionBook(string author, string name, decimal price) : base(author, name, price)
    {
    }

    public override decimal Price
    {
        get { return base.Price * (decimal) 1.3; }
    }
}