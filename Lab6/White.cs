using System;

namespace Lab6
{
   
    public delegate void Sorting(int[,] matrix);
    public delegate double BikeRide(double v, double a);
    public delegate int Finder(int[,] matrix);
    public delegate void SortRowsStyle(int[,] matrix);
    public delegate void ReplaceMaxElements(int[,] matrix, int value);
    public delegate int[,] GetTriangle(int[,] matrix);
    public delegate void SortRowsByMax(int[,] matrix);
    public delegate int FindNegatives(int[,] matrix);
    public delegate double MathInfo(int[,] matrix);
}

using System;
using System.Linq;

namespace Lab6
{
    public class White
    {
     
        

        public int FindMaxIndex(double[] array)
        {
            int maxIndex = 0;
            double maxValue = array[0];
            
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                    maxIndex = i;
                }
            }
            
            return maxIndex;
        }


        public int FindMaxRowIndexInColumn(int[,] matrix, int col)
        {
            int maxRow = 0;
            int maxValue = matrix[0, col];
            
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, col] > maxValue)
                {
                    maxValue = matrix[i, col];
                    maxRow = i;
                }
            }
            
            return maxRow;
        }


        public int[] GetNegativeCountPerRow(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int[] result = new int[rows];
            
            for (int i = 0; i < rows; i++)
            {
                int count = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        count++;
                    }
                }
                result[i] = count;
            }
            
            return result;
        }


        public int FindMax(int[,] matrix, out int row, out int col)
        {
            row = 0;
            col = 0;
            int maxValue = matrix[0, 0];
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                        row = i;
                        col = j;
                    }
                }
            }
            
            return maxValue;
        }


        public void SwapColumns(int[,] A, int colIndexA, int[,] B, int colIndexB)
        {
            if (A.GetLength(0) != B.GetLength(0))
            {
                return;
            }
            
            for (int i = 0; i < A.GetLength(0); i++)
            {
                int temp = A[i, colIndexA];
                A[i, colIndexA] = B[i, colIndexB];
                B[i, colIndexB] = temp;
            }
        }

 
        public void SortDiagonalAscending(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matrix[i, i] > matrix[j, j])
                    {
                        int temp = matrix[i, i];
                        matrix[i, i] = matrix[j, j];
                        matrix[j, j] = temp;
                    }
                }
            }
        }

        public void SortDiagonalDescending(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matrix[i, i] < matrix[j, j])
                    {
                        int temp = matrix[i, i];
                        matrix[i, i] = matrix[j, j];
                        matrix[j, j] = temp;
                    }
                }
            }
        }

  
        public long Factorial(int n)
        {
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }


        public double GetDistance(double v, double a)
        {
            double distance = 0;
            double currentV = v;
            
            for (int hour = 1; hour <= 10; hour++)
            {
                distance += currentV;
                currentV += a;
            }
            
            return distance;
        }

        public double GetTime(double v, double a)
        {
            double distance = 0;
            double currentV = v;
            int hours = 0;
            
            while (distance < 100 && hours < 1000)
            {
                distance += currentV;
                currentV += a;
                hours++;
            }
            
            return hours;
        }

   
        public void SwapFromLeft(double[] array)
        {
            for (int i = 0; i < array.Length - 1; i += 2)
            {
                double temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
            }
        }

        public void SwapFromRight(double[] array)
        {
            for (int i = array.Length - 1; i > 0; i -= 2)
            {
                double temp = array[i];
                array[i] = array[i - 1];
                array[i - 1] = temp;
            }
        }

        public double GetSum(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += array[i];
                }
            }
            return sum;
        }

 
        public int CountPositive(int[][] array)
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] > 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int FindMax(int[][] array)
        {
            int max = array[0][0];
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] > max)
                    {
                        max = array[i][j];
                    }
                }
            }
            return max;
        }

        public int FindMaxRowLength(int[][] array)
        {
            int maxLength = array[0].Length;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].Length > maxLength)
                {
                    maxLength = array[i].Length;
                }
            }
            return maxLength;
        }

      
        
        public void Task1(double[] A, double[] B)
        {
            int maxPosA = 0;
            double maxValueA = A[0];
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] > maxValueA)
                {
                    maxValueA = A[i];
                    maxPosA = i;
                }
            }
            

            int maxPosB = 0;
            double maxValueB = B[0];
            for (int i = 1; i < B.Length; i++)
            {
                if (B[i] > maxValueB)
                {
                    maxValueB = B[i];
                    maxPosB = i;
                }
            }
            
   
            int distanceA = A.Length - 1 - maxPosA;
            int distanceB = B.Length - 1 - maxPosB;
            
  
            double[] arrayToChange = A;
            int elementPos = maxPosA;
            
            if (distanceB > distanceA)
            {
                arrayToChange = B;
                elementPos = maxPosB;
            }
            else if (distanceB == distanceA)
            {
           
            }
            
 
            if (elementPos == arrayToChange.Length - 1)
            {
                return;
            }
            
  
            double total = 0;
            int count = 0;
            for (int i = elementPos + 1; i < arrayToChange.Length; i++)
            {
                total += arrayToChange[i];
                count++;
            }
            
            double average = total / count;
            
            arrayToChange[elementPos] = average;
        }

        public void Task2(int[,] A, int[,] B)
        {
      
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            {
                return;
            }
            
      
            int maxRowA = 0;
            int maxInColA = A[0, 0];
            for (int i = 1; i < A.GetLength(0); i++)
            {
                if (A[i, 0] > maxInColA)
                {
                    maxInColA = A[i, 0];
                    maxRowA = i;
                }
            }
            
 
            int maxRowB = 0;
            int maxInColB = B[0, 0];
            for (int i = 1; i < B.GetLength(0); i++)
            {
                if (B[i, 0] > maxInColB)
                {
                    maxInColB = B[i, 0];
                    maxRowB = i;
                }
            }
            
   
            for (int j = 0; j < A.GetLength(1); j++)
            {
                int temp = A[maxRowA, j];
                A[maxRowA, j] = B[maxRowB, j];
                B[maxRowB, j] = temp;
            }
        }

        public int Task3(int[,] matrix)
        {
            int answer = 0;
            
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            
            int[] negativeCount = new int[rowCount];
            
        
            for (int i = 0; i < rowCount; i++)
            {
                int count = 0;
                for (int j = 0; j < colCount; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        count++;
                    }
                }
                negativeCount[i] = count;
            }
            
           
            int maxCount = negativeCount[0];
            answer = 0;
            
            for (int i = 1; i < rowCount; i++)
            {
                if (negativeCount[i] > maxCount)
                {
                    maxCount = negativeCount[i];
                    answer = i;
                }
            }
            return answer;
        }

        public void Task4(int[,] A, int[,] B)
        {
       
            int rowA = 0;
            int colA = 0;
            int maxA = A[0, 0];
            
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] > maxA)
                    {
                        maxA = A[i, j];
                        rowA = i;
                        colA = j;
                    }
                }
            }
            

            int rowB = 0;
            int colB = 0;
            int maxB = B[0, 0];
            
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    if (B[i, j] > maxB)
                    {
                        maxB = B[i, j];
                        rowB = i;
                        colB = j;
                    }
                }
            }
            
           
            A[rowA, colA] = maxB;
            B[rowB, colB] = maxA;
        }

        public void Task5(int[,] A, int[,] B)
        {
           
            int rowA = 0;
            int colA = 0;
            int maxA = A[0, 0];
            
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] > maxA)
                    {
                        maxA = A[i, j];
                        rowA = i;
                        colA = j;
                    }
                }
            }
            
            
            int rowB = 0;
            int colB = 0;
            int maxB = B[0, 0];
            
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    if (B[i, j] > maxB)
                    {
                        maxB = B[i, j];
                        rowB = i;
                        colB = j;
                    }
                }
            }
            
           
            if (A.GetLength(0) != B.GetLength(0))
            {
                return;
            }
            
            
            for (int i = 0; i < A.GetLength(0); i++)
            {
                int temp = A[i, colA];
                A[i, colA] = B[i, colB];
                B[i, colB] = temp;
            }
        }

        public void Task6(int[,] matrix, Sorting sort)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                return;
            }
            
           
            sort(matrix);
        }

        public long Task7(int n, int k)
        {
            long answer = 0;
            
            if (k > n || n < 0 || k < 0)
            {
                return 0;
            }
            
            
            long factN = 1;
            for (int i = 2; i <= n; i++)
            {
                factN = factN * i;
            }
            
            
            long factK = 1;
            for (int i = 2; i <= k; i++)
            {
                factK = factK * i;
            }
            
            
            long factNK = 1;
            int diff = n - k;
            for (int i = 2; i <= diff; i++)
            {
                factNK = factNK * i;
            }
            
            
            answer = factN / (factK * factNK);
            return answer;
        }

        public double Task8(double v, double a, BikeRide ride)
        {
            double answer = 0;
            answer = ride(v, a);
            return answer;
        }

        public int Task9(int[][] array)
        {
            int answer = 0;
            
            if (array.Length % 2 == 0)
            {
               
                for (int i = 0; i < array.Length; i++)
                {
                    double[] tempArray = new double[array[i].Length];
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        tempArray[j] = array[i][j];
                    }
                    
                  
                    for (int j = 0; j < tempArray.Length - 1; j += 2)
                    {
                        double temp = tempArray[j];
                        tempArray[j] = tempArray[j + 1];
                        tempArray[j + 1] = temp;
                    }
                }
            }
            else
            {
               
                for (int i = 0; i < array.Length; i++)
                {
                    double[] tempArray = new double[array[i].Length];
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        tempArray[j] = array[i][j];
                    }
                    
                    
                    for (int j = tempArray.Length - 1; j > 0; j -= 2)
                    {
                        double temp = tempArray[j];
                        tempArray[j] = tempArray[j - 1];
                        tempArray[j - 1] = temp;
                    }
                }
            }
            
            
            double totalSum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 1) 
                {
                    double[] tempArray = new double[array[i].Length];
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        tempArray[j] = array[i][j];
                    }
                    
                    for (int j = 0; j < tempArray.Length; j++)
                    {
                        if (j % 2 == 0) 
                        {
                            totalSum += tempArray[j];
                        }
                    }
                }
            }
            
            answer = (int)totalSum;
            return answer;
        }

        public int Task10(int[][] array, Func<int[][], int> func)
        {
            int answer = 0;
            answer = func(array);
            return answer;
        }
    }
}
