namespace Pr01ReverseString_Lab
{
    using System;
    using System.Collections.Generic;
    
    public class Pr01ReverseString_Lab
    {
        public static void Main()
        {
            var inputString = Console.ReadLine();
            
            var stack = new Stack<char>(inputString.ToCharArray());

            while (stack.Count != 0)
            {
                Console.Write(stack.Pop());
            }
        }
    }
}