using System;
using System.Linq;

namespace Lab6
{
    public delegate void Sorting(int[,] matrix);
    public delegate double BikeRide(double v, double a);
    public delegate void Swapper(double[] array);

    public class White
    {
        // ===== Task 1 =====
        public int FindMaxIndex(double[] array)
        {
            int index = 0;
            for (int i = 1; i < array.Length; i++)
                if (array[i] > array[index])
                    index = i;
            return index;
        }

        public void Task1(double[] A, double[] B)
        {
            int maxA = FindMaxIndex(A);
            int maxB = FindMaxIndex(B);

            int distA = A.Length - 1 - maxA;
            int distB = B.Length - 1 - maxB;

            double[] target = distB > distA ? B : A;
            int maxIndex = distB > distA ? maxB : maxA;

            if (maxIndex == target.Length - 1) return;

            double avg = target.Skip(maxIndex + 1).Average();
            target[maxIndex] = avg;
        }

        // ===== Task 2 =====
        public int FindMaxRowIndexInColumn(int[,] matrix, int col)
        {
            int rows = matrix.GetLength(0);
            int maxRow = 0;
            for (int i = 1; i < rows; i++)
                if (matrix[i, col] > matrix[maxRow, col])
                    maxRow = i;
            return maxRow;
        }

        public void Task2(int[,] A, int[,] B)
        {
            if (A.GetLength(1) == 0 || B.GetLength(1) == 0) return;
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1)) return;

            int rowA = FindMaxRowIndexInColumn(A, 0);
            int rowB = FindMaxRowIndexInColumn(B, 0);

            for (int j = 0; j < A.GetLength(1); j++)
                (A[rowA, j], B[rowB, j]) = (B[rowB, j], A[rowA, j]);
        }

        // ===== Task 3 =====
        public int[] GetNegativeCountPerRow(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] result = new int[rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (matrix[i, j] < 0)
                        result[i]++;

            return result;
        }

        public int Task3(int[,] matrix)
        {
            int[] counts = GetNegativeCountPerRow(matrix);
            int maxIndex = 0;

            for (int i = 1; i < counts.Length; i++)
                if (counts[i] > counts[maxIndex])
                    maxIndex = i;

            return maxIndex;
        }

        // ===== Task 4 =====
        public int FindMax(int[,] matrix, out int row, out int col)
        {
            row = 0;
            col = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] > matrix[row, col])
                    {
                        row = i;
                        col = j;
                    }

            return matrix[row, col];
        }

        public void Task4(int[,] A, int[,] B)
        {
            int rA, cA, rB, cB;
            FindMax(A, out rA, out cA);
            FindMax(B, out rB, out cB);

            (A[rA, cA], B[rB, cB]) = (B[rB, cB], A[rA, cA]);
        }

        // ===== Task 5 =====
        public void SwapColumns(int[,] A, int colA, int[,] B, int colB)
        {
            for (int i = 0; i < A.GetLength(0); i++)
                (A[i, colA], B[i, colB]) = (B[i, colB], A[i, colA]);
        }

        public void Task5(int[,] A, int[,] B)
        {
            if (A.GetLength(0) != B.GetLength(0)) return;

            int rA, cA, rB, cB;
            FindMax(A, out rA, out cA);
            FindMax(B, out rB, out cB);

            SwapColumns(A, cA, B, cB);
        }

        // ===== Task 6 =====
        public void SortDiagonalAscending(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[] diag = new int[n];

            for (int i = 0; i < n; i++)
                diag[i] = matrix[i, i];

            Array.Sort(diag);

            for (int i = 0; i < n; i++)
                matrix[i, i] = diag[i];
        }

        public void SortDiagonalDescending(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[] diag = new int[n];

            for (int i = 0; i < n; i++)
                diag[i] = matrix[i, i];

            Array.Sort(diag);
            Array.Reverse(diag);

            for (int i = 0; i < n; i++)
                matrix[i, i] = diag[i];
        }

        public void Task6(int[,] matrix, Sorting sort)
        {
            sort(matrix);
        }

        // ===== Task 7 =====
        public long Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        public long Task7(int n, int k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        // ===== Task 8 =====
        public double GetDistance(double v, double a)
        {
            double distance = 0;
            for (int t = 0; t < 10; t++)
                distance += v + a * t;
            return distance;
        }

        public double GetTime(double v, double a)
        {
            double distance = 0;
            int t = 0;
            while (distance < 100)
            {
                distance += v + a * t;
                t++;
            }
            return t;
        }

        public double Task8(double v, double a, BikeRide ride)
        {
            return ride(v, a);
        }

        // ===== Task 9 =====
        public double Sum(double[] array)
        {
            double sum = 0;
            for (int i = 1; i < array.Length; i += 2)
                sum += array[i];
            return sum;
        }

        public void SwapFromLeft(double[] array)
        {
            for (int i = 0; i < array.Length - 1; i += 2)
                (array[i], array[i + 1]) = (array[i + 1], array[i]);
        }

        public void SwapFromRight(double[] array)
        {
            for (int i = array.Length - 1; i > 0; i -= 2)
                (array[i], array[i - 1]) = (array[i - 1], array[i]);
        }

        public int Task9(int[][] array)
        {
            Swapper swap = array.Length % 2 == 0 ? SwapFromLeft : SwapFromRight;

            foreach (var arr in array)
                swap(arr);

            int sum = 0;
            foreach (var arr in array)
                for (int i = 1; i < arr.Length; i += 2)
                    sum += arr[i];

            return sum;
        }

        // ===== Task 10 =====
        public int CountPositive(int[][] array) =>
            array.Sum(a => a.Count(x => x > 0));

        public int FindMax(int[][] array) =>
            array.Max(a => a.Max());

        public int FindMaxRowLength(int[][] array) =>
            array.Max(a => a.Length);

        public int Task10(int[][] array, Func<int[][], int> func)
        {
            return func(array);
        }
    }
}
