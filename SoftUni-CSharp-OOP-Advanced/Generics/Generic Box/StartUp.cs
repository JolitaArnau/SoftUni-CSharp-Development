using System;

public class StartUp
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            var number = Console.ReadLine();

            var box = new Box<string>(number);

            Console.WriteLine(box.ToString());
        }
    }
}