
namespace Pr05CalculateSequenceWithQueue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Pr05CalculateSequenceWithQueue
    {
        public static void Main()
        {
            var initialNumber = long.Parse(Console.ReadLine());

            var sequence = new Queue<long>();
           
            sequence.Enqueue(initialNumber);
            
            var result = new List<long>();
            
            result.Add(initialNumber);

            while (result.Count < 50)
            {
                var firstNumber = sequence.Dequeue();
                var secondNumber = firstNumber + 1;
                var thirdNumber = (2 * firstNumber) + 1;
                var fourthNumber = firstNumber + 2;
                
                sequence.Enqueue(secondNumber);
                sequence.Enqueue(thirdNumber);
                sequence.Enqueue(fourthNumber);
                
                result.Add(secondNumber);
                result.Add(thirdNumber);
                result.Add(fourthNumber);
            }

            for (int i = 0; i < 50; i++)
            {
                Console.Write(result[i] + " ");
            }
        }
    }
}