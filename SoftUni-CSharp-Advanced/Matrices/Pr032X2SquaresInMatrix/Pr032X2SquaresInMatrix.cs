namespace Pr032X2SquaresInMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    class Pr032X2SquaresInMatrix
    {
        static void Main()
        {
            var rowsAndCols = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            
            string[,] matrix = new string[rowsAndCols[0], rowsAndCols[1]];

            for (int rows = 0; rows < rowsAndCols[0]; rows++)
            {
                var letters = Console
                    .ReadLine()
                    .Split(' ')
                    .ToArray();

                for (int cols = 0; cols < rowsAndCols[1]; cols++)
                {
                    matrix[rows, cols] = letters[cols];
                }
            }
            
            var elemetsToCheck = new List<string>();
            
            bool isSquareWithEqualsElements = false;
            int counter = 0;

            for (int rows = 0; rows < matrix.GetLength(0) - 1; rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1) - 1; cols++)
                {
                    var firstLetter = matrix[rows, cols];
                    var secondLetter = matrix[rows, cols + 1];
                    var thirdLetter = matrix[rows + 1, cols];
                    var fourthLetter = matrix[rows + 1, cols + 1];

                    elemetsToCheck.Add(firstLetter);
                    elemetsToCheck.Add(secondLetter);
                    elemetsToCheck.Add(thirdLetter);
                    elemetsToCheck.Add(fourthLetter);
                    
                    int currentCounter = 0;

                    foreach (var element in elemetsToCheck)
                    {
                        
                        if (element.Equals(elemetsToCheck[0]))
                        {
                            currentCounter++;
                        }

                        if (currentCounter == 4)
                        {
                            isSquareWithEqualsElements = true;
                            counter++;
                        }
                    }
                    
                    elemetsToCheck.Clear();
                }
            }
            
            if (counter > 0  && isSquareWithEqualsElements)
            {
                Console.WriteLine(counter);
            }
            if (counter == 0 && !isSquareWithEqualsElements)
            {
                Console.WriteLine(0);
            }
        }
    }
}