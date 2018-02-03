namespace Pr03GroupNumbersLab
{
    using System;
    using System.Linq;
    
    class Pr03GroupNumbersLab
    {
        static void Main()
        {
            var numbers = Console
                .ReadLine()
                .Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Console.WriteLine(string.Join(" ", numbers.Where(n => Math.Abs(n) % 3 == 0)));
            Console.WriteLine(string.Join(" ", numbers.Where(n => Math.Abs(n) % 3 == 1)));
            Console.WriteLine(string.Join(" ", numbers.Where(n => Math.Abs(n) % 3 == 2)));
        }
    }
}