
namespace Pr03MaximumElement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pr03MaximumElement
    {
        public static void Main()
        {
            var queryCount = int.Parse(Console.ReadLine());
            
            var sequence = new Stack<int>();
            var maxNumbers = new Stack<int>();
            var maxElement = int.MinValue;

            for (int i = 0; i < queryCount; i++)
            {
                var query = Console
                    .ReadLine()
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                
                if (query.Length == 2)
                {
                    // push element at query[1]

                    var numberToPush = query[1];
                    
                    sequence.Push(numberToPush);

                    if (numberToPush >= maxElement)
                    {
                        maxElement = numberToPush;
                        maxNumbers.Push(maxElement);
                    }
                }

                if (query[0] == 2)
                {
                    // pop
                    
                    var poppedElement = sequence.Pop();
                    var currentMax = maxNumbers.Peek();

                    if (poppedElement == currentMax)
                    {
                        maxNumbers.Pop();

                        if (maxNumbers.Count > 0)
                        {
                            maxElement = maxNumbers.Peek();
                        }
                        else
                        {
                            maxElement = int.MinValue;
                        }
                    }
                   
                }
                
                if (query[0] == 3)
                {
                    // print max element/elements

                    Console.WriteLine(maxNumbers.Peek());
                  
                }
            }
        }
    }
}