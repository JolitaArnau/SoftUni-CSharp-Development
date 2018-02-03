
namespace Pr06TruckTour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Pr06TruckTour
    {
        public static void Main()
        {
            var petrolPumpsCount = int.Parse(Console.ReadLine());
            var pumps = new Queue<GasPump>();

            for (int i = 0; i < petrolPumpsCount; i++)
            {
                var tourInfo = Console
                    .ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var amountOfGas = tourInfo[0];
                var distanceToNext = tourInfo[1];
                
                GasPump pump = new GasPump(amountOfGas, distanceToNext, i);

                pumps.Enqueue(pump);
            }

            GasPump starterPump = null;
            bool completeTour = false;

            while (pumps.Count > 0)
            {
                GasPump currentPump = pumps.Dequeue();
                pumps.Enqueue(currentPump);
                
                starterPump = currentPump;
                var gasInTank = currentPump.amountOfGas;

                while (gasInTank >= currentPump.distanceToNext)
                {
                    gasInTank -= currentPump.distanceToNext;
                    
                    currentPump = pumps.Dequeue();
                    pumps.Enqueue(currentPump);

                    if (currentPump == starterPump)
                    {
                        completeTour = true;
                        break;
                    }

                    gasInTank += currentPump.amountOfGas;
                }
                
                if (completeTour)
                {
                    Console.WriteLine(currentPump.index);
                    break;
                }
            }
        }

        public class GasPump
        {
            public int amountOfGas;
            public int distanceToNext;
            public int index;

            public GasPump(int amountOfGas, int distanceToNext, int index)
            {
                this.amountOfGas = amountOfGas;
                this.distanceToNext = distanceToNext;
                this.index = index;
            }
        }
    }
}