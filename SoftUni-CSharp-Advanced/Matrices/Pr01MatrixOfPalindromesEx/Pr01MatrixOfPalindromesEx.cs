using System;
using System.Linq;

namespace Pr01MatrixOfPalindromesEx
{
    class Pr01MatrixOfPalindromesEx
    {
        static void Main()
        {
            var rowsAndCols = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var rows = rowsAndCols[0];
            var cols = rowsAndCols[1];

            for (int rowCounter = 0; rowCounter < rows; rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < cols; columnCounter++)
                {
                    Console.Write("" + (char)('a' + rowCounter) 
                                  + (char)('a' + rowCounter + columnCounter) 
                                  + (char)('a' + rowCounter) + ' ');
                }

                Console.WriteLine();
            }
        }
    }
}