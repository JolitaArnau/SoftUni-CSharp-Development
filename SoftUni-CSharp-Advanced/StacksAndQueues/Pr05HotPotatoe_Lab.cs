namespace Pr05HotPotatoe_Lab
{
    using System;
    using System.Collections.Generic;
    
    public class Pr05HotPotatoe_Lab
    {
        public static void Main()
        {
            var input = Console
                .ReadLine()
                .Split();
                
            var children = new Queue<string>(input);

            var tossLimit = int.Parse(Console.ReadLine());

            while (children.Count != 1)
            {
                for (int i = 1; i < tossLimit; i++)
                {
                    children.Enqueue(children.Dequeue());
                }

                Console.WriteLine($"Removed {children.Dequeue()}");
            }

            Console.WriteLine($"Last is {children.Dequeue()}");
        }
    }
}