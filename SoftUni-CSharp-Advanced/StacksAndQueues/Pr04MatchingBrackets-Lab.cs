using System;
using System.Collections.Generic;

namespace Pr04MatchingBrackets_Lab
{
    public class Pr04MatchingBrackets_Lab
    {
        public static void Main()
        {
            // opening bracket -> push index to stack
            // closing bracket -> pop topmost from stack; index of opening bracket
            // use current and popped to find expression 
            
            // (2 + 3) - (2 + 3)
            // 
            
            // from 0 to

            var expression = Console.ReadLine();
            
            var openBracketIndeces = new Stack<int>();

            for (int i = 0; i < expression.Length; i++)
            {
                var currentElement = expression[i];

                if (currentElement == '(')
                {
                    openBracketIndeces.Push(i);
                }
                
                else if (currentElement == ')')
                {
                    int openBracketIndex = openBracketIndeces.Pop();

                    int expressionLength = i - openBracketIndex + 1;

                    var subexrpession = expression.Substring(openBracketIndex, expressionLength);

                    Console.WriteLine(subexrpession);
                }
            }
        }
    }
}