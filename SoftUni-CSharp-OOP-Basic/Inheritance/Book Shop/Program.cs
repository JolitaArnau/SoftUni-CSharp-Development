using System;

public class Program
{
    public static void Main()
    {
        var author = Console.ReadLine();
        var title = Console.ReadLine();
        var price = decimal.Parse(Console.ReadLine());

        try
        {
            Book book = new Book(author, title, price);
            GoldenEditionBook goldenEditionBook = new GoldenEditionBook(author, title, price);

            Console.WriteLine(book);
            Console.WriteLine(goldenEditionBook);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}