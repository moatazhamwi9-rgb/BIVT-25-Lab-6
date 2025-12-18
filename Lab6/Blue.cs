using System;

namespace Lab6
{
    public delegate int Finder(int[,] matrix, out int row, out int col);
    public delegate int FindElement(int[,] matrix, out int row, out int col);
    public delegate void SortRowsStyle(int[,] matrix, int row);
    public delegate void ReplaceMaxElements(int[,] matrix, int row, int maxValue);
    public delegate int[] GetTriangle(int[,] matrix);

    public class Blue
    {
        public void Task1(ref int[,] matrix)
        {
            // code here
            if (matrix == null || matrix.GetLength(0) == 0)
            {
                return;
            }
            if (!IsSquare(matrix))
            {
                // для неквадратных – ничего не делаем
                // это для пул реквеста но все плохо
                return;
            }

            int rowIndex = FindDiagonalMaxIndex(matrix);
            if (rowIndex < 0 || rowIndex >= matrix.GetLength(0))
            {
                return;
            }

            RemoveRow(ref matrix, rowIndex);
            // end
        }
        public int Task2(int[,] A, int[,] B, int[,] C)
        {
            int answer = 0; // 1 - increasing   0 - no sequence   -1 - decreasing

            // code here
            double[] values = new double[3];
            values[0] = GetAverageExceptEdges(A);
            values[1] = GetAverageExceptEdges(B);
            values[2] = GetAverageExceptEdges(C);

            bool increasing = values[0] < values[1] && values[1] < values[2];
            bool decreasing = values[0] > values[1] && values[1] > values[2];
            if (increasing)
            {
                answer = 1;
            }
            else if (decreasing)
            {
                answer = -1;
            }

            return answer;
            // end
        }
        public void Task3(ref int[,] matrix, Func<int[,], int> method)
        {
            // code here
            if (matrix == null || method == null)
            {
                return;
            }
            if (!IsSquare(matrix))
            {
                return;
            }

            int col = method(matrix);
            if (col < 0 || col >= matrix.GetLength(1))
            {
                return;
            }

            RemoveColumn(ref matrix, col);
            // end
        }
        public void Task4(ref int[,] matrix)
        {
            // code here
            if (matrix == null)
            {
                matrix = null;
                return;
            }

            int col = 0;
            while (col < matrix.GetLength(1))
            {
                if (!CheckZerosInColumn(matrix, col))
                {
                    RemoveColumn(ref matrix, col);
                }
                else
                {
                    col++;
                }
            }
            // end
        }
        public void Task5(ref int[,] matrix, Finder find)
        {
            // code here
            if (matrix == null || find == null)
            {
                matrix = null;
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows == 0 || cols == 0)
            {
                matrix = new int[rows, cols];
                return;
            }

            int targetRow;
            int targetCol;
            int targetValue = find(matrix, out targetRow, out targetCol);
            if (targetRow < 0 || targetRow >= rows || targetCol < 0 || targetCol >= cols)
            {
                matrix = null;
                return;
            }

            bool[] remove = new bool[rows];
            int removeCount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == targetValue)
                    {
                        remove[i] = true;
                        removeCount++;
                        break;
                    }
                }
            }

            if (removeCount == 0)
            {
                return;
            }

            int newRows = rows - removeCount;
            if (newRows < 0)
            {
                newRows = 0;
            }

            int[,] result = new int[newRows, cols];
            int newRowIndex = 0;
            for (int i = 0; i < rows; i++)
            {
                if (remove[i])
                {
                    continue;
                }

                for (int j = 0; j < cols; j++)
                {
                    result[newRowIndex, j] = matrix[i, j];
                }

                newRowIndex++;
            }

            matrix = result;
            // end
        }
        public void Task6(int[,] matrix, SortRowsStyle sort)
        {
            // code here
            if (matrix == null || sort == null)
            {
                return;
            }

            for (int row = 0; row < matrix.GetLength(0); row += 3)
            {
                sort(matrix, row);
            }
            // end
        }
        public void Task7(int[,] matrix, ReplaceMaxElements transform)
        {
            // code here
            if (matrix == null || transform == null)
            {
                return;
            }

            int rows = matrix.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                int maxValue = FindMaxInRow(matrix, i);
                transform(matrix, i, maxValue);
            }
            // end
        }
        public double[,] Task8(double a, double b, double h, Func<double, double> getSum, Func<double, double> getY)
        {
            double[,] answer = null;

            // code here
            if (getSum == null || getY == null || h <= 0 || double.IsNaN(a) || double.IsNaN(b) || b < a)
            {
                return answer;
            }

            var sums = new System.Collections.Generic.List<(double sum, double y)>();
            int guard = 0;
            for (double x = a; x <= b + 1e-9 && guard < 10000; x += h, guard++)
            {
                sums.Add((getSum(x), getY(x)));
            }

            answer = new double[sums.Count, 2];
            for (int i = 0; i < sums.Count; i++)
            {
                answer[i, 0] = sums[i].sum;
                answer[i, 1] = sums[i].y;
            }

            return answer;
            // end
        }
        public int Task9(int[,] matrix, GetTriangle triangle)
        {
            int answer = 0;

            // code here
            if (!IsSquare(matrix) || triangle == null)
            {
                return answer;
            }

            answer = GetSum(triangle, matrix);
            return answer;
            // end
        }
        public bool Task10(int[][] array, Predicate<int[][]> func)
        {
            bool res = false;

            // code here
            if (array == null || func == null)
            {
                return res;
            }

            res = func(array);

            return res;
            // end
        }

        public int FindDiagonalMaxIndex(int[,] matrix)
        {
            if (!IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return 0;
            }

            int size = matrix.GetLength(0);
            int maxValue = matrix[0, 0];
            int rowIndex = 0;
            for (int i = 1; i < size; i++)
            {
                int current = matrix[i, i];
                if (current > maxValue)
                {
                    maxValue = current;
                    rowIndex = i;
                }
            }

            return rowIndex;
        }

        public void RemoveRow(ref int[,] matrix, int rowIndex)
        {
            if (matrix == null)
            {
                matrix = null;
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rowIndex < 0 || rowIndex >= rows)
            {
                matrix = null;
                return;
            }

            int newRows = rows - 1;
            if (newRows < 0)
            {
                newRows = 0;
            }

            int[,] result = new int[newRows, cols];
            int newRow = 0;
            for (int i = 0; i < rows; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                for (int j = 0; j < cols; j++)
                {
                    result[newRow, j] = matrix[i, j];
                }

                newRow++;
            }

            matrix = result;
        }

        public double GetAverageExceptEdges(int[,] matrix)
        {
            if (matrix == null)
            {
                return 0;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows == 0 || cols == 0)
            {
                return 0;
            }

            long sum = 0;
            int min = matrix[0, 0];
            int max = matrix[0, 0];
            int count = rows * cols;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int value = matrix[i, j];
                    sum += value;
                    if (value < min)
                    {
                        min = value;
                    }

                    if (value > max)
                    {
                        max = value;
                    }
                }
            }

            if (count <= 2)
            {
                return 0;
            }

            double numerator = sum - min - max;
            int denominator = count - 2;
            if (denominator == 0)
            {
                return 0;
            }

            return numerator / denominator;
        }

        public int FindUpperColIndex(int[,] matrix)
        {
            if (!IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return 0;
            }

            int size = matrix.GetLength(0);
            bool found = false;
            int maxVal = 0;
            int colIndex = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    int value = matrix[i, j];
                    if (!found || value > maxVal)
                    {
                        maxVal = value;
                        colIndex = j;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                return 0;
            }

            return colIndex;
        }

        public int FindLowerColIndex(int[,] matrix)
        {
            if (!IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return 0;
            }

            int size = matrix.GetLength(0);
            int maxVal = matrix[0, 0];
            int colIndex = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int value = matrix[i, j];
                    if (value > maxVal)
                    {
                        maxVal = value;
                        colIndex = j;
                    }
                }
            }

            return colIndex;
        }

        public void RemoveColumn(ref int[,] matrix, int col)
        {
            if (matrix == null)
            {
                matrix = null;
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (col < 0 || col >= cols)
            {
                matrix = null;
                return;
            }

            int newCols = cols - 1;
            if (newCols < 0)
            {
                newCols = 0;
            }

            int[,] result = new int[rows, newCols];
            for (int i = 0; i < rows; i++)
            {
                int newCol = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (j == col)
                    {
                        continue;
                    }

                    result[i, newCol] = matrix[i, j];
                    newCol++;
                }
            }

            matrix = result;
        }

        public bool CheckZerosInColumn(int[,] matrix, int col)
        {
            if (matrix == null)
            {
                return false;
            }

            int rows = matrix.GetLength(0);
            if (col < 0 || col >= matrix.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, col] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public int FindMax(int[,] matrix, out int row, out int col)
        {
            row = -1;
            col = -1;
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return 0;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int maxVal = matrix[0, 0];
            row = 0;
            col = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int value = matrix[i, j];
                    if (value > maxVal)
                    {
                        maxVal = value;
                        row = i;
                        col = j;
                    }
                }
            }

            return maxVal;
        }

        public int FindMax(int[,] matrix)
        {
            int row;
            int col;
            return FindMax(matrix, out row, out col);
        }

        public int FindMin(int[,] matrix, out int row, out int col)
        {
            row = -1;
            col = -1;
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return 0;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int minVal = matrix[0, 0];
            row = 0;
            col = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int value = matrix[i, j];
                    if (value < minVal)
                    {
                        minVal = value;
                        row = i;
                        col = j;
                    }
                }
            }

            return minVal;
        }

        public int FindMin(int[,] matrix)
        {
            int row;
            int col;
            return FindMin(matrix, out row, out col);
        }

        public void SortRowAscending(int[,] matrix, int row)
        {
            if (!IsValidRow(matrix, row))
            {
                return;
            }

            int cols = matrix.GetLength(1);
            int[] values = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                values[j] = matrix[row, j];
            }

            Array.Sort(values);
            for (int j = 0; j < cols; j++)
            {
                matrix[row, j] = values[j];
            }
        }

        public void SortRowDescending(int[,] matrix, int row)
        {
            if (!IsValidRow(matrix, row))
            {
                return;
            }

            int cols = matrix.GetLength(1);
            int[] values = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                values[j] = matrix[row, j];
            }

            Array.Sort(values);
            Array.Reverse(values);
            for (int j = 0; j < cols; j++)
            {
                matrix[row, j] = values[j];
            }
        }

        public int FindMaxInRow(int[,] matrix, int row)
        {
            if (!IsValidRow(matrix, row))
            {
                return 0;
            }

            int cols = matrix.GetLength(1);
            int maxVal = matrix[row, 0];
            for (int j = 1; j < cols; j++)
            {
                if (matrix[row, j] > maxVal)
                {
                    maxVal = matrix[row, j];
                }
            }

            return maxVal;
        }

        public void ReplaceByZero(int[,] matrix, int row, int maxValue)
        {
            if (!IsValidRow(matrix, row))
            {
                return;
            }

            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                if (matrix[row, j] == maxValue)
                {
                    matrix[row, j] = 0;
                }
            }
        }

        public void MultiplyByColumn(int[,] matrix, int row, int maxValue)
        {
            if (!IsValidRow(matrix, row))
            {
                return;
            }

            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                if (matrix[row, j] == maxValue)
                {
                    matrix[row, j] = maxValue * (j + 1);
                }
            }
        }

        public double[,] GetSumAndY(double a, double b, double h, Func<double, double> sum, Func<double, double> y)
        {
            return Task8(a, b, h, sum, y);
        }

        public double SumA(double x)
        {
            double sum = 1.0;
            double factorial = 1.0;
            for (int i = 1; i <= 50; i++)
            {
                factorial *= i;
                double term = Math.Cos(i * x) / factorial;
                sum += term;
                if (Math.Abs(term) < 1e-6)
                {
                    break;
                }
            }

            return sum;
        }

        public double YA(double x)
        {
            return Math.Exp(Math.Cos(x)) * Math.Cos(Math.Sin(x));
        }

        public double SumB(double x)
        {
            double sum = 0.0;
            for (int i = 1; i <= 10000; i++)
            {
                double term = Math.Cos(i * x) / (i * i);
                if (i % 2 == 1)
                {
                    sum -= term;
                }
                else
                {
                    sum += term;
                }

                if (Math.Abs(term) < 1e-6)
                {
                    break;
                }
            }

            // смещение на постоянную составляющую
            sum -= (2.0 * Math.PI * Math.PI) / 3.0;
            return sum;
        }

        public double YB(double x)
        {
            return (x * x) / 4.0 - (3.0 * Math.PI * Math.PI) / 4.0;
        }

        public int Sum(int[] array)
        {
            if (array == null)
            {
                return 0;
            }

            long sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += (long)array[i] * array[i];
            }

            if (sum > int.MaxValue)
            {
                return int.MaxValue;
            }

            if (sum < int.MinValue)
            {
                return int.MinValue;
            }

            return (int)sum;
        }

        public int GetSum(GetTriangle transformer, int[,] matrix)
        {
            if (transformer == null || !IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return 0;
            }

            int[] values = transformer(matrix);
            return Sum(values);
        }

        public int[] GetUpperTriangle(int[,] matrix)
        {
            if (!IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return Array.Empty<int>();
            }

            int size = matrix.GetLength(0);
            int length = size * (size + 1) / 2;
            int[] result = new int[length];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    result[index] = matrix[i, j];
                    index++;
                }
            }

            return result;
        }

        public int[] GetLowerTriangle(int[,] matrix)
        {
            if (!IsSquare(matrix) || matrix.GetLength(0) == 0)
            {
                return Array.Empty<int>();
            }

            int size = matrix.GetLength(0);
            int length = size * (size + 1) / 2;
            int[] result = new int[length];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    result[index] = matrix[i, j];
                    index++;
                }
            }

            return result;
        }

        public bool CheckTransformAbility(int[][] array)
        {
            if (array == null)
            {
                return false;
            }

            int rows = array.Length;
            if (rows == 0)
            {
                return false;
            }

            int total = 0;
            for (int i = 0; i < rows; i++)
            {
                if (array[i] == null)
                {
                    return false;
                }

                total += array[i].Length;
            }

            return total % rows == 0;
        }

        public bool CheckSumOrder(int[][] array)
        {
            if (array == null)
            {
                return false;
            }

            int rows = array.Length;
            if (rows <= 1)
            {
                return true;
            }

            int[] sums = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                if (array[i] == null)
                {
                    return false;
                }

                int currentSum = 0;
                for (int j = 0; j < array[i].Length; j++)
                {
                    currentSum += array[i][j];
                }

                sums[i] = currentSum;
            }

            bool increasing = true;
            bool decreasing = true;
            for (int i = 1; i < rows; i++)
            {
                if (sums[i - 1] >= sums[i])
                {
                    increasing = false;
                }

                if (sums[i - 1] <= sums[i])
                {
                    decreasing = false;
                }
            }

            return increasing || decreasing;
        }

        public bool CheckArraysOrder(int[][] array)
        {
            if (array == null)
            {
                return false;
            }

            for (int i = 0; i < array.Length; i++)
            {
                int[] current = array[i];
                if (current == null)
                {
                    return false;
                }

                if (current.Length <= 1)
                {
                    return true;
                }

                bool increasing = true;
                bool decreasing = true;
                for (int j = 1; j < current.Length; j++)
                {
                    if (current[j - 1] >= current[j])
                    {
                        increasing = false;
                    }

                    if (current[j - 1] <= current[j])
                    {
                        decreasing = false;
                    }
                }

                if (increasing || decreasing)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSquare(int[,] matrix)
        {
            if (matrix == null)
            {
                return false;
            }

            return matrix.GetLength(0) == matrix.GetLength(1);
        }

        private bool IsValidRow(int[,] matrix, int row)
        {
            if (matrix == null)
            {
                return false;
            }

            return row >= 0 && row < matrix.GetLength(0) && matrix.GetLength(1) > 0;
        }
    }
}

