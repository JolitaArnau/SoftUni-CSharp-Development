namespace Pr02SquareWithMaximumSum
{
    using System;
    using System.Linq;
    
    class Pr02SquareWithMaximumSum
    {
        static void Main()
        {
            var rowsAndCols = Console
                .ReadLine()
                .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            int[,] matrix = new int[rowsAndCols[0], rowsAndCols[1]];

            for (int rows = 0; rows < rowsAndCols[0]; rows++)
            {
                var rowValues = Console
                    .ReadLine()
                    .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int cols = 0; cols < rowsAndCols[1]; cols++)
                {
                    matrix[rows, cols] = rowValues[cols];
                }
            }

            int finalSum = 0;
            int rowIndex = 0;
            int columnIndex = 0;

            for (int rows = 0; rows < matrix.GetLength(0) - 1; rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1) - 1; cols++)
                {
                    int tempSum = matrix[rows, cols] + matrix[rows, cols + 1] + matrix[rows + 1, cols] +
                                  matrix[rows + 1, cols + 1];

                    if (tempSum > finalSum)
                    {
                        finalSum = tempSum;
                        rowIndex = rows;
                        columnIndex = cols;
                    }
                }
            }

            Console.WriteLine(matrix[rowIndex, columnIndex] + " " + matrix[rowIndex, columnIndex + 1]);
            Console.WriteLine(matrix[rowIndex + 1, columnIndex] + " " + matrix[rowIndex + 1, columnIndex + 1]);
            Console.WriteLine(finalSum);
        }
    }
}