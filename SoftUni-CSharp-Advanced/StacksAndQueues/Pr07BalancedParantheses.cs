namespace Pr07BalancedParantheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Pr07BalancedParantheses
    {
        public static void Main()
        {
            var parantheses = Console
                .ReadLine()
                .ToCharArray();

            var openingBraces = new char[]
            {
                '(',
                '[',
                '{'
            };
            
            var closingBraces = new char[]
            {
                ')',
                ']',
                '}'
            };
            
            var paranthesesStack = new Stack<char>();

            for (int i = 0; i < parantheses.Length; i++)
            {
                if (parantheses[i].Equals(openingBraces[0]) ||
                    parantheses[i].Equals(openingBraces[1]) ||
                    parantheses[i].Equals(openingBraces[2]))
                {
                    paranthesesStack.Push(parantheses[i]);
                }

                if (parantheses[i].Equals(closingBraces[0]) ||
                    parantheses[i].Equals(closingBraces[1]) ||
                    parantheses[i].Equals(closingBraces[2]))
                {
                    var popped = paranthesesStack.Pop();

                    if (!popped.Equals(parantheses[i]))
                    {
                        continue;
                    }

                    if (paranthesesStack.Count == 0)
                    {
                        Console.WriteLine("NO");
                    }

                    if (popped.Equals(parantheses[i]))
                    {
                        Console.WriteLine("NO");
                    }

                }
            }

            if (paranthesesStack.Count == 0)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}