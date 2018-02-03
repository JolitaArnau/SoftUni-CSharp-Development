namespace Pr03DecimalToBinaryConverter_Lab
{
    using System;
    using System.Collections.Generic;
    
    public class Pr03DecimalToBinaryConverter_Lab
    {
        public static void Main()
        {
            var elementToConvert = int.Parse(Console.ReadLine());
            var convertedElement = new Stack<int>();

            if (elementToConvert == 0)
            {
                Console.WriteLine(0);
            }

            while (elementToConvert > 0)
            {
                convertedElement.Push(elementToConvert % 2);

                elementToConvert /= 2;
            }

            while (convertedElement.Count > 0)
            {
                Console.Write(convertedElement.Pop());
            }
        }
    }
}