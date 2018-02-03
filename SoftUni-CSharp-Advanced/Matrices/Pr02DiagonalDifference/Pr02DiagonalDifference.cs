
namespace Pr02DiagonalDifference
{
    using System;
    using System.Linq;
    
    class Pr02DiagonalDifference
    {
        static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());

            long[][] matrix = new long[matrixSize][];

            for (int i = 0; i < matrixSize; i++)
            {
                matrix[i] = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            }

            //Primary diagonal:
            long primarySum = 0;

            for (int row = 0; row < matrixSize; row++)
            {
                primarySum += matrix[row][row];
            }

            //Secondary diagonal:
            long secondarySum = 0;

            for (int row = 0, col = matrixSize - 1; row < matrixSize; row++, col--)
            {
                secondarySum += matrix[row][col];
            }

            Console.WriteLine(Math.Abs(primarySum - secondarySum));
        }
    }
}