namespace Pr02SimpleCalculator_Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var values = input.Split(' ');

            var stack = new Stack<string>(values.Reverse());

            while (stack.Count > 1)
            {
                var firstOperator = int.Parse(stack.Pop());

                var operand = stack.Pop();

                var secondOperator = int.Parse(stack.Pop());

                switch (operand)
                {
                    case "+":
                        stack.Push((firstOperator + secondOperator).ToString());
                        break;
                        
                    case "-":
                        stack.Push((firstOperator - secondOperator).ToString());
                        break;      
                }
            }

            Console.WriteLine(stack.Pop());
        }
    }
}