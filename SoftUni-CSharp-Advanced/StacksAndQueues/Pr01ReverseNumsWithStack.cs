
namespace Pr01ReverseNumsWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Pr01ReverseNumsWithStack
    {
        public static void Main()
        {
            var inputNumbers = Console
                .ReadLine()
                .Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var stackNums = new Stack<int>();
            
            foreach (var num in inputNumbers)
            {
                stackNums.Push(num);
            }

            while (stackNums.Count != 0)
            {
                var currentNum = stackNums.Pop();

                Console.Write(currentNum);

                Console.Write(" ");
            }
        }
    }
}