namespace FunctionalProgrammingLab
{
    using System;
    using System.Linq;
    
    class Pr01EvenNumbers
    {
         static void Main()
        {
            var numbers = Console
                .ReadLine()
                .Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var result = numbers
                .Where(n => n % 2 == 0)
                .OrderBy(n => n)
                .ToList();

            Console.WriteLine(string.Join(", ", result)); 
        }
    }
}