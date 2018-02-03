namespace Pr04BasicQueueOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Pr04BasicQueueOperations
    {
        public static void Main()
        { 
            var operations = Console
                .ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var enqueue = operations[0];
            var dequeue = operations[1];
            var numberToCheck = operations[2];

            var elements = new int[enqueue];
            elements = Console
                .ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var numbersFromArray = new Queue<int>();

            foreach (var element in elements)
            {
                numbersFromArray.Enqueue(element);
            }

            var debug = 0;

            if (numbersFromArray.Count != 0)
            {
                for (int i = 0; i < dequeue; i++)
                {
                    numbersFromArray.Dequeue();
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