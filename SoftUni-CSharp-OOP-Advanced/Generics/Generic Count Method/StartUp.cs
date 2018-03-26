using System;

class StartUp
{
    static void Main()
    {
        var count = int.Parse(Console.ReadLine());

        var box = new Box<double>();

        for (int i = 0; i < count; i++)
        { 
            var element = double.Parse(Console.ReadLine());

            box.Add(element);
        }

        var queryElement = double.Parse(Console.ReadLine());

        Console.WriteLine(box.GetElementsCountGreaterThanProvidedElement(box.Sequence, queryElement));
    }
}