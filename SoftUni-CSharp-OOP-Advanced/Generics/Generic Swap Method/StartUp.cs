using System;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var data = new Box<int>();

        var count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            var input = int.Parse(Console.ReadLine());
            data.Add(input);
        }

        var indeces = Console.ReadLine().Split().Select(int.Parse).ToArray();
        data.Swap(indeces[0], indeces[1]);

        Console.WriteLine(data);
    }
}