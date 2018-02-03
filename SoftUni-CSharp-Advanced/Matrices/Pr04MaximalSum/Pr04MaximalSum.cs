

namespace Pr04MaximalSum
{
    using System;
    using System.Linq;
    
    class Pr04MaximalSum
    {
        static void Main()
        {
            var rowsAndCols = Console
                .ReadLine()
                .Split(new [] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            int[,] matrix = new int[rowsAndCols[0], rowsAndCols[1]];

            for (int rows = 0; rows < rowsAndCols[0]; rows++)
            {
                var matrixValues = Console
                    .ReadLine()
                    .Split(new [] {" "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int cols = 0; cols < rowsAndCols[1]; cols++)
                {
                    matrix[rows, cols] = matrixValues[cols];
                }
            }

            int finalSum = 0;
            int rowIndex = 0;
            int colIndex = 0;

            for (int row = 0; row < matrix.GetLength(0) - 2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 2; col++)
                {
                    var tempSum = matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2]
                                  + matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2]
                                  + matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];

                    if (tempSum > finalSum)
                    {
                        finalSum = tempSum;
                        rowIndex = row;
                        colIndex = col;
                    }
                }
            }

            Console.WriteLine($"Sum = {finalSum}");
            Console.WriteLine($"{matrix[rowIndex, colIndex]} {matrix[rowIndex, colIndex + 1]} {matrix[rowIndex, colIndex + 2]}");
            Console.WriteLine($"{matrix[rowIndex + 1, colIndex]} {matrix[rowIndex + 1, colIndex + 1]} {matrix[rowIndex + 1, colIndex + 2]}");
            Console.WriteLine($"{matrix[rowIndex + 2, colIndex]} {matrix[rowIndex + 2, colIndex + 1]} {matrix[rowIndex + 2, colIndex + 2]}");
        }
    }
}