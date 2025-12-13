using System;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6
{
    public delegate void Sorting(int[] matrix);
    public delegate void SortRowsByMax(int[,] matrix);
    public delegate int[] FindNegatives(int[,] matrix);
    public delegate int[,] MathInfo(int[,] matrix);
    
    public class Purple
    {
        // Вспомогательные методы для задачи 1
        public int FindDiagonalMaxIndex(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int maxIndex = 0;
            int maxValue = matrix[0, 0];
            
            for (int i = 1; i < n; i++)
            {
                if (matrix[i, i] > maxValue)
                {
                    maxValue = matrix[i, i];
                    maxIndex = i;
                }
            }
            
            return maxIndex;
        }
        
        public void SwapRowColumn(int[,] A, int rowIndex, int[,] B, int columnIndex)
        {
            int n = A.GetLength(0);
            int f = B.GetLength(0);
            
            for (int i = 0; i < n; i++)
            {
                int temp = A[rowIndex, i];
                A[rowIndex, i] = B[i, columnIndex];
                B[i, columnIndex] = temp;
            }
        }
        
        public void Task1(int[,] A, int[,] B)
        {
            int rowIndex = FindDiagonalMaxIndex(A);
            int columnIndex = FindDiagonalMaxIndex(B);
            if ((A.GetLength(0) == B.GetLength(0)))

                SwapRowColumn(A, rowIndex, B, columnIndex);
        }
        
        // Вспомогательные методы для задачи 2
        public int CountPositiveElementsInRow(int[,] matrix, int row)
        {
            int count = 0;
            int cols = matrix.GetLength(1);
            
            for (int j = 0; j < cols; j++)
            {
                if (matrix[row, j] > 0) count++;
            }
            
            return count;
        }
        
        public int CountPositiveElementsInColumn(int[,] matrix, int col)
        {
            int count = 0;
            int rows = matrix.GetLength(0);
            
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, col] > 0) count++;
            }
            
            return count;
        }
        
        public void InsertColumn(ref int[,] A, int rowIndex, int columnIndex, int[,] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            
            int[,] newMatrix = new int[rowsA + 1, colsA];
            
            for (int i = 0; i <= rowIndex; i++)
            {
                for (int j = 0; j < colsA; j++)
                {
                    newMatrix[i, j] = A[i, j];
                }
            }
            
            for (int j = 0; j < colsA; j++)
            {
                if (j < rowsB)
                    newMatrix[rowIndex + 1, j] = B[j, columnIndex];
                else
                    newMatrix[rowIndex + 1, j] = 0;
            }
            
            for (int i = rowIndex + 1; i < rowsA; i++)
            {
                for (int j = 0; j < colsA; j++)
                {
                    newMatrix[i + 1, j] = A[i, j];
                }
            }
            
            A = newMatrix;
        }
        
        public void Task2(ref int[,] A, int[,] B)
        {
        
            int rowsA = A.GetLength(0);
            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);
            if (A.GetLength(1) != B.GetLength(0))
                return;
            
            int maxRowIndex = 0;
            int maxRowCount = CountPositiveElementsInRow(A, 0);
            
            for (int i = 0; i < rowsA; i++)
            {
                int count = CountPositiveElementsInRow(A, i);
                if (count > maxRowCount)
                {
                    maxRowCount = count;
                    maxRowIndex = i;
                }
            }
            
            int maxColIndex = -1;
            int maxColCount = -1;
            
            for (int j = 0; j < colsB; j++)
            {
                int count = CountPositiveElementsInColumn(B, j);
                if (count > maxColCount)
                {
                    maxColCount = count;
                    maxColIndex = j;
                }
            }
            
            if (maxColCount <= 0)
                return;
                
            InsertColumn(ref A, maxRowIndex, maxColIndex, B);
        }
        
        public void ChangeMatrixValues(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int totalElements = rows * cols;
            
            if (totalElements < 5)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = matrix[i, j] * 2;
                    }
                }
                return;
            }
            
            int[] maxIndices = new int[5];
            int[] maxAbsValues = new int[5];
            
            for (int i = 0; i < 5; i++)
            {
                maxAbsValues[i] = int.MinValue;
            }
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int absValue = matrix[i, j];
                    int flatIndex = i * cols + j;
                    
                    for (int k = 0; k < 5; k++)
                    {
                        if (absValue > maxAbsValues[k])
                        {
                            for (int m = 4; m > k; m--)
                            {
                                maxAbsValues[m] = maxAbsValues[m - 1];
                                maxIndices[m] = maxIndices[m - 1];
                            }
                            
                            maxAbsValues[k] = absValue;
                            maxIndices[k] = flatIndex;
                            break;
                        }
                    }
                }
            }
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int flatIndex = i * cols + j;
                    bool isTop5 = false;
                    
                    for (int k = 0; k < 5; k++)
                    {
                        if (maxIndices[k] == flatIndex)
                        {
                            isTop5 = true;
                            break;
                        }
                    }
                    
                    if (isTop5)
                    {
                        if (matrix[i, j] >= 0)
                            matrix[i, j] = matrix[i, j] * 2;
                        else
                            matrix[i, j] = -(Math.Abs(matrix[i, j]) * 2);
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j] / 2;
                    }
                }
            }
        }
        
        public void Task3(int[,] matrix)
        {
            ChangeMatrixValues(matrix);
        }
        
        // Вспомогательные методы для задачи 4
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
        
        public void Task4(int[,] A, int[,] B)
        {
            int[] negCountA = CountNegativesPerRow(A);
            int[] negCountB = CountNegativesPerRow(B);
            
            // Проверяем, есть ли отрицательные элементы
            bool hasNegativesA = negCountA.Any(c => c > 0);
            bool hasNegativesB = negCountB.Any(c => c > 0);
            
            if (!hasNegativesA || !hasNegativesB)
                return;
                
            int maxRowA = FindMaxIndex(negCountA);
            int maxRowB = FindMaxIndex(negCountB);
            
            int colsA = A.GetLength(1);
            int colsB = B.GetLength(1);
            if (colsA != colsB)
                return;
            int minCols = Math.Min(colsA, colsB);
            
            for (int j = 0; j < minCols; j++)
            {
                int temp = A[maxRowA, j];
                A[maxRowA, j] = B[maxRowB, j];
                B[maxRowB, j] = temp;
            }
        }
        
        // Методы для задачи 5
        public void SortNegativeAscending(int[] array)
        {
            List<(int value, int index)> negatives = new List<(int, int)>();
            
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    negatives.Add((array[i], i));
                }
            }
            
            var sortedNegatives = negatives.OrderBy(n => n.value).ToList();
            
            for (int i = 0; i < negatives.Count; i++)
            {
                int originalIndex = negatives[i].index;
                array[originalIndex] = sortedNegatives[i].value;
            }
        }
        
        public void SortNegativeDescending(int[] array)
        {
            List<(int value, int index)> negatives = new List<(int, int)>();
            
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    negatives.Add((array[i], i));
                }
            }
            
            var sortedNegatives = negatives.OrderByDescending(n => n.value).ToList();
            
            for (int i = 0; i < negatives.Count; i++)
            {
                int originalIndex = negatives[i].index;
                array[originalIndex] = sortedNegatives[i].value;
            }
        }
        
        public void Task5(int[] array, Sorting sort)
        {
            sort(array);
        }
        
        // Вспомогательные методы для задачи 6
        public int GetRowMax(int[,] matrix, int row)
        {
            int cols = matrix.GetLength(1);
            int max = matrix[row, 0];
            
            for (int j = 1; j < cols; j++)
            {
                if (matrix[row, j] > max)
                {
                    max = matrix[row, j];
                }
            }
            
            return max;
        }
        
        public void SortRowsByMaxAscending(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            List<(int[] row, int max)> rowsData = new List<(int[], int)>();
            
            for (int i = 0; i < rows; i++)
            {
                int[] row = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = matrix[i, j];
                }
                rowsData.Add((row, GetRowMax(matrix, i)));
            }
            
            rowsData = rowsData.OrderBy(r => r.max).ToList();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rowsData[i].row[j];
                }
            }
        }
        
        public void SortRowsByMaxDescending(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            List<(int[] row, int max)> rowsData = new List<(int[], int)>();
            
            for (int i = 0; i < rows; i++)
            {
                int[] row = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = matrix[i, j];
                }
                rowsData.Add((row, GetRowMax(matrix, i)));
            }
            
            rowsData = rowsData.OrderByDescending(r => r.max).ToList();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rowsData[i].row[j];
                }
            }
        }
        
        public void Task6(int[,] matrix, SortRowsByMax sort)
        {
            sort(matrix);
        }
        
        // Методы для задачи 7
        public int[] FindNegativeCountPerRow(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] result = new int[rows];
            
            for (int i = 0; i < rows; i++)
            {
                int count = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < 0) count++;
                }
                result[i] = count;
            }
            
            return result;
        }
        
        public int[] FindMaxNegativePerColumn(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] result = new int[cols];
            
            for (int j = 0; j < cols; j++)
            {
                int maxNeg = 0; 
                bool hasNegative = false;
                
                for (int i = 0; i < rows; i++)
                {
                    if (matrix[i, j] < 0)
                    {
                        if (!hasNegative || matrix[i, j] > maxNeg)
                        {
                            maxNeg = matrix[i, j];
                            hasNegative = true;
                        }
                    }
                }
                
                result[j] = hasNegative ? maxNeg : 0;
            }
            
            return result;
        }
        
        public int[] Task7(int[,] matrix, FindNegatives find)
        {
            return find(matrix);
        }
        
        // Методы для задачи 8
        public int[,] DefineSeq(int[,] matrix)
        {
            int cols = matrix.GetLength(1);
            
            if (cols <= 1)
                return new int[,] { };
                
            bool allEqual = true;
            for (int j = 1; j < cols; j++)
            {
                if (matrix[1, j] != matrix[1, j - 1])
                {
                    allEqual = false;
                    break;
                }
            }
            
            if (allEqual)
                return new int[0, 0];
                
            bool increasing = true;
            bool decreasing = true;
            
            for (int j = 1; j < cols; j++)
            {
                if (matrix[1, j] < matrix[1, j - 1])
                    increasing = false;
                if (matrix[1, j] > matrix[1, j - 1])
                    decreasing = false;
            }
            
            int[,] result = new int[1, 1];
            if (increasing && decreasing)
                result[0, 0] = 2;
            else if (increasing)
                result[0, 0] = 1;
            else if (decreasing)
                result[0, 0] = -1;
            else
                result[0, 0] = 0;
                
            return result;
        }
        
        public int[,] FindAllSeq(int[,] matrix)
        {
            int cols = matrix.GetLength(1);
            
            if (cols <= 1)
                return new int[,] { };
                
            List<(int start, int end)> intervals = new List<(int, int)>();
            int start = 0;
            
            for (int j = 1; j < cols; j++)
            {
                if (j == cols - 1)
                {
                    if (j > start)
                    {
                        intervals.Add((matrix[0, start], matrix[0, j]));
                    }
                }
                else if ((matrix[1, j] > matrix[1, j - 1] && matrix[1, j + 1] < matrix[1, j]) ||
                         (matrix[1, j] < matrix[1, j - 1] && matrix[1, j + 1] > matrix[1, j]))
                {
                    if (j > start)
                    {
                        intervals.Add((matrix[0, start], matrix[0, j]));
                    }
                    start = j;
                }
            }
            
            intervals = intervals.OrderBy(i => i.start).ThenBy(i => i.end).ToList();
            
            int[,] result = new int[intervals.Count, 2];
            for (int i = 0; i < intervals.Count; i++)
            {
                result[i, 0] = intervals[i].start;
                result[i, 1] = intervals[i].end;
            }
            
            return result;
        }
        
        public int[,] FindLongestSeq(int[,] matrix) 
        { 
            int cols = matrix.GetLength(1); 
 
            if (cols <= 1) 
                return new int[,] { }; 
                 
            int longestStart = 0; 
            int longestEnd = 0; 
            int currentStart = 0; 
            int maxLength = 0; 
            int orien = 0; 
             
            for (int j = 1; j < cols; j++) 
            { 
                if (((orien != 1) && (matrix[1, j] <= matrix[1, j - 1])) || ((orien != -1) && (matrix[1, j] >= matrix[1, j - 1]))) 
                { 
                    if (orien == 0) 
                    { 
                        if (matrix[1, j] > matrix[1, j - 1]) 
                            orien = 1; 
                        else if (matrix[1, j] < matrix[1, j - 1]) 
                            orien = -1; 
                    } 
 
                } 
                else 
                { 
                    currentStart = j - 1; 
                    if (matrix[1, j] < matrix[1, j - 1]) 
                        orien = -1; 
                    else if (matrix[1, j] > matrix[1, j - 1]) 
                        orien = 1; 
                    else 
                        orien = 0; 
                } 
                 
                if ((matrix[0, j] - matrix[0, currentStart]) > maxLength) 
                { 
                    maxLength = matrix[0, j] - matrix[0, currentStart]; 
                    longestStart = currentStart; 
                    longestEnd = j; 
                } 
                 
            } 
             
            int[,] result = new int[1, 2]; 
            result[0, 0] = matrix[0, longestStart]; 
            result[0, 1] = matrix[0, longestEnd]; 
             
            return result; 
        }
        
        public int[,] Task8(int[,] matrix, MathInfo info)
        {
            return info(matrix);
        }
        
        // Методы для задачи 9
        public double FuncA(double x)
        {
            return x * x - Math.Sin(x);
        }
        
        public double FuncB(double x)
        {
            return Math.Exp(x) - 1;
        }
        
        public int CountSignFlips(double a, double b, double h, Func<double, double> func)
        {
            if (h <= 0 || a > b)
                return 0;
                
            int signFlips = 0;
            double prevValue = func(a);
            
            for (double x = a + h; x <= b + 1e-10; x += h)
            {
                double currentValue = func(x);
                
                if (Math.Sign(prevValue) != Math.Sign(currentValue))
                {
                    if (Math.Sign(currentValue) != 0)
                        signFlips++;
                }
                if (Math.Sign(currentValue) != 0)
                    prevValue = currentValue;
            }
            
            return signFlips;
        }
        
        public int Task9(double a, double b, double h, Func<double, double> func)
        {
            return CountSignFlips(a, b, h, func);
        }
        
        // Методы для задачи 10
        public void SortInCheckersOrder(int[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    if (i % 2 == 0)
                    {
                        Array.Sort(array[i]);
                    }
                    else
                    {
                        Array.Sort(array[i]);
                        Array.Reverse(array[i]);
                    }
                }
            }
        }
        
        public void SortBySumDesc(int[][] array)
        {
            var sumsWithIndices = new List<(int sum, int index)>();
            
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    int sum = array[i].Sum();
                    sumsWithIndices.Add((sum, i));
                }
                else
                {
                    sumsWithIndices.Add((0, i));
                }
            }
            
            sumsWithIndices.Sort((x, y) => y.sum.CompareTo(x.sum));
            
            int[][] sortedArray = new int[array.Length][];
            for (int i = 0; i < array.Length; i++)
            {
                sortedArray[i] = array[sumsWithIndices[i].index];
            }
            
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sortedArray[i];
            }
        }
        
        public void TotalReverse(int[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    Array.Reverse(array[i]);
                }
            }
            
            Array.Reverse(array);
        }
        
        public void Task10(int[][] array, Action<int[][]> func)
        {
            func(array);
        }
    }

}
