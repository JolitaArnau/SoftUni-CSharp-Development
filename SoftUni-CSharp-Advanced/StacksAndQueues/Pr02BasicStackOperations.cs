namespace Pr02BasicStackOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class Pr02BasicStackOperations
    {
        public static void Main()
        {
            var operations = Console
                .ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var push = operations[0];
            var pop = operations[1];
            var numberToCheck = operations[2];

            var elements = new int[push];
            elements = Console
                .ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var numbersFromArray = new Stack<int>();

            foreach (var element in elements)
            {
                numbersFromArray.Push(element);
            }

            if (numbersFromArray.Count != 0)
            {
                for (int i = 0; i < pop; i++)
                {
                    numbersFromArray.Pop();
                }

            }
            
            if (numbersFromArray.Count != 0 && numbersFromArray.Contains(numberToCheck))
            {
                Console.WriteLine("true");
            }
            if (numbersFromArray.Count != 0 && !numbersFromArray.Contains(numberToCheck))
            {
                Console.WriteLine(numbersFromArray.Min());
            }

            if (numbersFromArray.Count == 0)
            {
                Console.WriteLine(0);
            }
        }
    }
}