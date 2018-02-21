using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        var length = double.Parse(Console.ReadLine());
        var widht = double.Parse(Console.ReadLine());
        var height = double.Parse(Console.ReadLine());

        try
        {
            Box box = new Box(length, widht, height);

            var surface = box.CalculateSurfaceArea(length, widht, height);
            var lateralSurface = box.CalculateLateralSurface(length, widht, height);
            var volume = box.CalculateVolume(length, widht, height);

            Console.WriteLine($"Surface Area - {surface:f2}");
            Console.WriteLine($"Lateral Surface Area - {lateralSurface:f2}");
            Console.WriteLine($"Volume - {volume:f2}");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}