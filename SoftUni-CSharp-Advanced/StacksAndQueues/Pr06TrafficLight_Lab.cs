namespace Pr06TrafficLight_Lab
{
    using System;
    using System.Collections.Generic;
    
    public class Pr06TrafficLight_Lab
    {
        public static void Main()
        {
            var carsThatCanPassLimit = int.Parse(Console.ReadLine());

            var input = Console.ReadLine();
            
            var carsQueue = new Queue<string>();

            var carsThatPassedTotal = 0;

            while (!input.Equals("end"))
            {
                if (input.Equals("green"))
                {
                    var carsThatWillActuallyPass = Math.Min(carsQueue.Count, carsThatCanPassLimit);
                    
                    for (int i = 0; i < carsThatWillActuallyPass; i++)
                    {
                        Console.WriteLine($"{carsQueue.Dequeue()} passed!");

                     
                        carsThatPassedTotal++;
                    }
                }

                else
                {
                    carsQueue.Enqueue(input);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine($"{carsThatPassedTotal} cars passed the crossroads.");
        }
    }
}