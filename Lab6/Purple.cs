using System;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6
{
    public class Purple
    {

        public void printMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
        public void printRow(int[,] matrix, int row)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                Console.Write($"{matrix[row, i]} ");
            }
            Console.WriteLine();
        }
        
        public void printCol(int[,] matrix, int col)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write($"{matrix[i, col]} ");
            }
            Console.WriteLine("\n");
        }
        
        public int FindDiagonalMaxIndex(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int MaxI = 0;
            for (int i = 1; i < n; i++)
            {
                if (matrix[i, i] > matrix[MaxI, MaxI]) MaxI = i;
            }

            return MaxI;
        }

        public void SwapRowColumn(int[,] matrix, int rowIndex, int[,] B, int columnIndex)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                (matrix[rowIndex, i], B[i, columnIndex]) = (B[i, columnIndex], matrix[rowIndex, i]);
            }
        }
        
        public void Task1(int[,] A, int[,] B)
        {

            // code here
            if (A.GetLength(0) == A.GetLength(1) && B.GetLength(0) == B.GetLength(1) &&
                B.GetLength(0) == A.GetLength(1))
            {
                
                int MaxInxA = FindDiagonalMaxIndex(A);
                int MaxInxB = FindDiagonalMaxIndex(B);
                printMatrix(A);
                printMatrix(B);
                SwapRowColumn(A, MaxInxA, B, MaxInxB);
                printMatrix(A);
                printMatrix(B);
            }
            
            // end

        }

        public void InsertColumn(ref int[,] A, int rowIndex, int columnIndex, int[,] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            int[,] matrix = new int[rows + 1, cols];
            
            for (int row = 0; row <= rowIndex; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = A[row, col];
                }
            }
            
            for (int col = 0; col < cols; col++)
            {
                matrix[rowIndex + 1, col] = B[col, columnIndex];
            }
            
            for (int row = rowIndex + 1; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row + 1, col] = A[row, col];
                }
            }

            A = matrix;
        }
        
        public int CountPositiveElementsInRow(int[,] matrix, int row)
        {
            int cnt = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[row, i] > 0) cnt++;
            }
            return cnt;
        }
        
        public int CountPositiveElementsInColumn(int[,] matrix, int col)
        {
            int cnt = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, col] > 0) cnt++;
            }
            return cnt;
        }
        
        public void Task2(ref int[,] A, int[,] B)
        {
            
            // code here
            
            if (A.GetLength(1) != B.GetLength(0)) return;
            
            int rows = A.GetLength(0);
            
            printMatrix(A);
            
            printMatrix(B);
            
            int MaxRow = 0;
            int MaxCnt = 0;
            for (int row = 0; row < rows; row++)
            {
                int cnt = CountPositiveElementsInRow(A, row);
                if (cnt > MaxCnt)
                {
                    MaxCnt = cnt;
                    MaxRow = row;
                }
            }
            
            int cols = B.GetLength(1);
            int MaxCol = 0;
            MaxCnt = 0;
            for (int col = 0; col < cols; col++)
            {
                int cnt = CountPositiveElementsInColumn(B, col);
                if (cnt > MaxCnt)
                {
                    MaxCnt = cnt;
                    MaxCol = col;
                }
            }

            if (MaxCnt == 0) return;

            printCol(B, MaxCol);

            InsertColumn(ref A, MaxRow, MaxCol, B);

            printMatrix(A);
            // end

        }

        public void ChangeMatrixValues(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] MaxValues = new int[5];

            for (int i = 0; i < 5; i++)
            {
                MaxValues[i] = int.MinValue;
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int MinInx = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (MaxValues[i] < MaxValues[MinInx])
                        {
                            MinInx = i;
                        }
                    }

                    if (matrix[row, col] > MaxValues[MinInx])
                    {
                        MaxValues[MinInx] = matrix[row, col];
                    }
                }
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    bool flag = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (MaxValues[i] == matrix[row, col])
                        {
                            matrix[row, col] *= 2;
                            MaxValues[i] = int.MinValue;
                            flag = false;
                            break;
                        }
                    }
                    if (flag) matrix[row, col] /= 2;
                }
            }
        }
        public void Task3(int[,] matrix)
        {

            // code here
            printMatrix(matrix);
            ChangeMatrixValues(matrix);
            printMatrix(matrix);
            // end

        }

        public int[] CountNegativesPerRow(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] counts = new int[rows];
            
            for (int i = 0; i < rows; i++)
            {
                int count = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < 0) count++;
                }
                counts[i] = count;
            }
            
            return counts;
        }
        
        public int FindMaxIndex(int[] array)
        {
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        public void SwapRows(int[,] A, int RowA, int[,] B, int RowB)
        {
            for (int col = 0; col < A.GetLength(1); col++)
            {
                (A[RowA, col], B[RowB, col]) = (B[RowB, col], A[RowA, col]);
            }
        } 
        
        public void Task4(int[,] A, int[,] B)
        {

            // code here
            if(A.GetLength(1) != B.GetLength(1)) return;
            
            printMatrix(A);
            printMatrix(B);
            
            int[] arrA = CountNegativesPerRow(A);
            int[] arrB = CountNegativesPerRow(B);
            
            if (arrA.All(cnt => cnt == 0) || arrB.All(cnt => cnt == 0)) return;
            
            int A_Row = FindMaxIndex(arrA);
            int B_Row = FindMaxIndex(arrB);

            SwapRows(A, A_Row, B, B_Row);
            printMatrix(A);
            printMatrix(B);
            
            
            
            // end

        }

        public delegate void Sorting(int[] array);

        public void SortNegativeAscending(int[] matrix)
        {
            int n = matrix.Length;
            int[] negatives = new int [matrix.Count(el => el < 0)];
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i] < 0) negatives[cnt++] = matrix[i];
            }

            Array.Sort(negatives);

            cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i] < 0) matrix[i] = negatives[cnt++];
            }
        }

        public void SortNegativeDescending(int[] matrix)
        {
            int n = matrix.Length;
            int[] negatives = new int [matrix.Count(el => el < 0)];
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i] < 0) negatives[cnt++] = matrix[i];
            }

            Array.Sort(negatives);
            Array.Reverse(negatives);

            cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i] < 0) matrix[i] = negatives[cnt++];
            }
        }
        
        public void Task5(int[] matrix, Sorting sort)
        {

            // code here
            Console.WriteLine(string.Join(" ", matrix) + "\n");
            sort(matrix);
            Console.WriteLine(string.Join(" ", matrix) + "\n");
            // end

        }
        public delegate void SortRowsByMax(int[,] matrix);

        public void SortRowsByMaxAscending(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int row = 0; row < rows - 1; row++)
                {
                    int Max1 = GetRowMax(matrix, row);
                    int Max2 = GetRowMax(matrix, row + 1);
                    ;

                    if (Max1 > Max2)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            (matrix[row, col], matrix[row + 1, col]) = (matrix[row + 1, col], matrix[row, col]);
                        }
                    }
                }
            }
        }

        public void SortRowsByMaxDescending(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                
                for (int row = 0; row < rows-1; row++)
                {
                    int Max1 = GetRowMax(matrix, row);
                    int Max2 = GetRowMax(matrix, row + 1);

                    if (Max1 < Max2)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            (matrix[row, col], matrix[row + 1, col]) = (matrix[row + 1, col], matrix[row, col]);
                        }
                    }
                }
                printMatrix(matrix);
            }
        }

        public int GetRowMax(int[,] matrix, int row)
        {
            int Max = int.MinValue;
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col] > Max) Max = matrix[row, col];
            }
            return Max;
        }
        public void Task6(int[,] matrix, SortRowsByMax sort)
        {

            // code here
            printMatrix(matrix);
            sort(matrix);
            printMatrix(matrix);
            // end

        }

        public delegate int[] FindNegatives(int[,] matrix);

        public int[] FindNegativeCountPerRow(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] arr = new int [rows];

            for (int row = 0; row < rows; row++)
            {
                int cnt = 0;
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] < 0) cnt++;
                }

                arr[row] = cnt;
            }

            return arr;
        }
        public int[] FindMaxNegativePerColumn(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] arr = new int [cols];

            for (int col = 0; col < cols; col++)
            {
                int Max = int.MinValue;
                for (int row = 0; row < rows; row++)
                {
                    if (matrix[row, col] < 0 && matrix[row, col] > Max) Max = matrix[row, col];
                }

                if (Max == int.MinValue) Max = 0;
                arr[col] = Max;
            }

            return arr;
        }
        public int[] Task7(int[,] matrix, FindNegatives find)
        {
            int[] negatives = null;

            // code here
            printMatrix(matrix);
            negatives = find(matrix);
            Console.WriteLine(string.Join(" ", negatives));
            // end

            return negatives;
        }

        public delegate int[,] MathInfo(int[,] matrix);

        public int[,] DefineSeq(int[,] matrix)
        {
            int[,] result = new int [1, 1];
            int cols = matrix.GetLength(1);
            if (cols > 1)
            {
                if (matrix[1, 0] > matrix[1, 1])
                {
                    bool flag = true;
                    for (int col = 1; col < cols - 1; col++)
                    {
                        if (matrix[1, col] < matrix[1, col + 1])
                            flag = false;
                    }

                    if (flag)
                        result[0, 0] = -1;
                }
                else if (matrix[1, 0] < matrix[1, 1])
                {
                    bool flag = true;
                    for (int col = 1; col < cols - 1; col++)
                    {
                        if (matrix[1, col] > matrix[1, col + 1])
                            flag = false;
                    }

                    if (flag)
                        result[0, 0] = 1;
                }
            }
            else return new int[,] { };
             return result;
        }

        public int[,] FindAllSeq(int[,] matrix)
        {
            
            int cols = matrix.GetLength(1);
            if (cols <= 1) return new int[0, 0];

            List<int[]> intervals = new List<int[]>();
            int startX = matrix[0, 0];
            int currentDir = 0;
            int nextDir= 0;
            for (int col = 0; col < cols - 1; col++)
            {
                
                if (matrix[1, col] < matrix[1, col + 1]) nextDir = 1; 
                else if (matrix[1, col] > matrix[1, col + 1]) nextDir = -1; 

                if (currentDir != 0 && nextDir != currentDir)
                {
                    intervals.Add(new int[] { startX, matrix[0, col] });
                    startX = matrix[0, col]; 
                }
                if (currentDir == 0)
                {
                    startX = matrix[0, col];
                }
                currentDir = nextDir;
            }
  
            intervals.Add(new int[] { startX, matrix[0, cols - 1] });
            
            var sortedIntervals = intervals.OrderBy(arr => arr[0]).ThenBy(arr => arr[1]).ToList();
            
            int[,] result = new int[sortedIntervals.Count, 2];
            for (int i = 0; i < sortedIntervals.Count; i++)
            {
                result[i, 0] = sortedIntervals[i][0];
                result[i, 1] = sortedIntervals[i][1];
            }

            return result;
        }
        public int[,] FindLongestSeq(int[,] matrix)
        {
            int[,] intervals = FindAllSeq(matrix);
            int len = intervals.GetLength(0);
            
            if (len == 0) return new int [,] {};

            int MaxLen = 0;
            int[,] MaxInterval = new int[1,2];
            for (int i = 0; i < len; i++)
            {
                if (intervals[i, 1] - intervals[i, 0] > MaxLen)
                {
                    MaxLen = intervals[i, 1] - intervals[i, 0];
                    MaxInterval[0,0] = intervals[i, 0];
                    MaxInterval[0,1] = intervals[i, 1];
                }
            }
            
            return MaxInterval;
        }
        public int[,] Task8(int[,] matrix, MathInfo info)
        {
            int[,] answer = null;

            // code here
            printMatrix(matrix);
            answer = info(matrix);
            printMatrix(answer);
            // end

            return answer;
        }

        public int CountSignFlips(double a, double b, double h, Func<double, double> func)
        {
            int cnt = 0;
            double sign = func(a) / Math.Abs(func(a));
            for (double i = a; i <= b; i += h)
            {
                Math.Round(i, 5);
                if (sign != func(i) / Math.Abs(func(i)))
                {
                    sign *= -1;
                    cnt++;
                }
            }

            return cnt;
        }

        public double FuncA(double x)
        {
            return x*x - Math.Sin(x);
        }
        
        public double FuncB(double x)
        {
            return Math.Exp(x) - 1;
        }
        public int Task9(double a, double b, double h, Func<double, double> func)
        {
            int answer = 0;

            // code here
            Console.WriteLine($"a:{a}, b:{b}, h:{h}");
            answer = CountSignFlips(a, b, h, func);
            Console.WriteLine(answer);
            // end

            return answer;
        }

        public void SortInCheckersOrder(int[][] array)
        {
            int i = 0;
            foreach (int[] arr in array)
            {
                Array.Sort(arr);
                if (i++ % 2 == 1) Array.Reverse(arr);
            }
        }

        public void SortBySumDesc(int[][] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].Sum() < array[j + 1].Sum())
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
        }

        public void TotalReverse(int[][] array)
        {
            foreach (int[] arr in array)
            {
                Array.Reverse(arr);
            }
            Array.Reverse(array);
        }
        public void Task10(int[][] array, Action<int[][]> func)
        {

            // code here
            func(array);
            // end

        }
    }
}