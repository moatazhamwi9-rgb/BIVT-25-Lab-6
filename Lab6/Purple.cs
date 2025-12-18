using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6
{
    public class Purple
    {
        public void Task1(int[,] A, int[,] B)
        {

            // code here
            int ind1 = FindDiagonalMaxIndex(A);
            int ind2 = FindDiagonalMaxIndex(B);
            SwapRowColumn(A, ind1, B, ind2);
            // end

        }
        public void Task2(ref int[,] A, int[,] B)
        {

            // code here
            int mx1 = 0;
            int ind1 = -1;
            int mx2 = 0;
            int ind2 = -1;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                int k = CountPositiveElementsInRow(A, i);
                if (k > mx1)
                {
                    mx1 = k;
                    ind1 = i;
                }
            }
            for (int i = 0; i < B.GetLength(1); i++)
            {
                int k = CountPositiveElementsInColumn(B, i);
                if (k > mx2)
                {
                    mx2 = k;
                    ind2 = i;
                }
            }
            int[,] ans = new int[A.GetLength(0) + 1, A.GetLength(1)];
            if (A.GetLength(1) == B.GetLength(0) && mx2 > 0)
            {
                InsertColumn(ref A, ind1, ind2, B);
            }
            
            // end

        }
        public void Task3(int[,] matrix)
        {

            // code here
            ChangeMatrixValues(matrix);
            // end

        }
        public void Task4(int[,] A, int[,] B)
        {

            // code here
            int ind1 = -1;
            int ind2 = -1;
            int x1 = A.GetLength(0);
            int x2 = B.GetLength(0);
            int y1 = A.GetLength(1);
            int y2 = B.GetLength(1);
            int[] term1 = CountNegativesPerRow(A);
            int[] term2 = CountNegativesPerRow(B);
            ind1 = FindMaxIndex(term1);
            ind2 = FindMaxIndex(term2);
            if (ind1 != -1 && ind2 != -1 && y1 == y2)
            {
                for (int j = 0; j < y1; j++)
                {
                    int t = A[ind1, j];
                    A[ind1, j] = B[ind2, j];
                    B[ind2, j] = t;
                }
            }

            // end

        }
        public void Task5(int[] matrix, Sorting sort)
        {

            // code here

            ProcessArray(matrix, sort);
            
            // end

        }
        public delegate void Sorting(int[] array);
        public void ProcessArray(int[] array, Sorting sort)
        {
            sort(array);
        }
        public void SortNegativeAscending(int[] matrix)
        {
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix[i] > matrix[j] && matrix[i] < 0 && matrix[j] < 0)
                    {
                        int temp = matrix[i];
                        matrix[i] = matrix[j];
                        matrix[j] = temp;
                    }
                }
            }
        }
        public void SortNegativeDescending(int[] matrix)
        {
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix[i] < matrix[j] && matrix[i] < 0 && matrix[j] < 0)
                    {
                        int temp = matrix[i];
                        matrix[i] = matrix[j];
                        matrix[j] = temp;
                    }
                }
            }
        }
        public void Task6(int[,] matrix, SortRowsByMax sort)
        {

            // code here
            int[,] cl1 = Clon(matrix);
            Help(matrix, sort);
           
            // end

        }
        public int[] Task7(int[,] matrix, FindNegatives find)
        {
            int[] negatives = null;

            // code here
            negatives = ProcessMatrix(matrix, find);
            // end

            return negatives;
        }
        public delegate int[] FindNegatives(int[,] matrix);
        public int[] ProcessMatrix(int[,] matrix, FindNegatives finder)
        {
            return finder(matrix);
        }
        public int[] FindNegativeCountPerRow(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int[] ans = new int[x];
            for (int i = 0; i < x; i++)
            {
                int k = 0;
                for (int j = 0; j < y; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        k++;
                    }
                }
                ans[i] = k;
            }
            return ans;
        }
        public int[] FindMaxNegativePerColumn(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int[] ans = new int[y];
            for (int j = 0; j < y; j++)
            {
                int mx = int.MinValue;
                int k = 0;
                for (int i = 0; i < x; i++)
                {
                    if (matrix[i, j] < 0 && matrix[i, j] > mx)
                    {
                        mx = matrix[i, j];
                        k++;
                    }
                }
                if (k > 0) ans[j] = mx;
                else ans[j] = 0;
            }
            return ans;
        }
        public int[,] Task8(int[,] matrix, MathInfo info)
        {
            int[,] answer = null;

            // code here
            answer = Vos(matrix, info);
            // end

            return answer;
        }
        public delegate int[,] MathInfo(int[,] matrix);
        public int[,] Vos(int[,] matrix, MathInfo info)
        {
            return info(matrix);
        }
        public bool Ravn(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int p = matrix[0, 0];
            for (int j = 1; j < y; j++)
            {
                if (matrix[0, j] != p)
                {
                    return false;
                }
            }
            return true;
        }
        public int[,] FindLongestSeq(int[,] matrix)
        {
            int[,] ans = new int[1, 2] { { 0, 0 } };
            if (Ravn(matrix))
            {
                return new int[,] { };
            }
            int[,] ints = FindAllSeq(matrix);
            if (ints.GetLength(0) == 0)
            {
                return new int[,] { };
            }
            int ind = 0;
            int mx = ints[0, 1] - ints[0, 0];
            for (int i = 1; i < ints.GetLength(0); i++)
            {
                int l = ints[i, 1] - ints[i, 0];
                if (l > mx)
                {
                    mx = l;
                    ind = i;
                }
            }
            ans = new int[1, 2] { { ints[ind, 0], ints[ind, 1] } };
            return ans;
        }
        public int[,] DefineSeq(int[,] matrix)
        {
            if (Ravn(matrix)) return new int[,] { };
            bool pr = true;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < j; i++)
                {
                    if (matrix[1, i] > matrix[1, j])
                    {
                        pr = false;
                        break;
                    }
                }
            }
            if (pr) return new int[,] { { 1 } };
            pr = true;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < j; i++)
                {
                    if (matrix[1, i] < matrix[1, j])
                    {
                        pr = false;
                        break;
                    }
                }
            }
            if (pr) return new int[,] { { -1 } };
            return new int[,] { { 0 } };
        }
        public int[,] FindAllSeq(int[,] matrix)
        {
            if (Ravn(matrix)) return new int[,] { };
            if (DefineSeq(matrix)[0, 0] != 0)
            {
                int[,] res = new int[1, 2];
                res[0, 0] = matrix[0, 0];
                res[0, 1] = matrix[0, matrix.GetLength(1) - 1];
                return res;
            }
            int k = 0;
            for (int j = 1; j + 1 < matrix.GetLength(1); j++)
            {
                if ((matrix[1, j] > matrix[1, j + 1] && matrix[1, j] > matrix[1, j - 1]) ||
                    (matrix[1, j] < matrix[1, j + 1] && matrix[1, j] < matrix[1, j - 1]))
                {
                    k++;
                }
            }
            int[] count = new int[k + 2];
            int ind = 1;
            for (int j = 1; j + 1 < matrix.GetLength(1); j++)
            {
                if ((matrix[1, j] > matrix[1, j + 1] && matrix[1, j] > matrix[1, j - 1]) ||
                    (matrix[1, j] < matrix[1, j + 1] && matrix[1, j] < matrix[1, j - 1]))
                {
                    count[ind] = matrix[0, j];
                    ind++;
                }
            }
            count[0] = matrix[0, 0];
            count[count.Length - 1] = matrix[0, matrix.GetLength(1) - 1];
            ind = 0;
            int[,] ans = new int[k + 1, 2];
            for (int i = 0; i < ans.GetLength(0); i++)
            {
                for (int j = 0; j < ans.GetLength(1); j++)
                {
                    ans[i, j] = count[ind];
                    ind++;
                }
                ind--;
            }
            return ans;
        }
        public int Task9(double a, double b, double h, Func<double, double> func)
        {
            int answer = 0;

            // code here
            answer = CountSignFlips(a, b, h, func);
            // end

            return answer;
        }
        public delegate void Func(double x);
        public double FuncA(double x)
        {
            double ans = x * x - Math.Sin(x);
            return ans;
        }
        public double FuncB(double x)
        {
            double ans = Math.Pow(Math.E, x) - 1;
            return ans;
        }
        public int CountSignFlips(double a, double b, double h, Func<double, double> func)
        {
            if (h <= 0) return 0;
            if (a > b) return 0;

            int count = 0;
            double prev = 0;
            bool pr = true;
            for (double x = a; x <= b + 0.00001; x += h)
            {
                double pr1 = func(x);
                double pr2 = func(x + h);
                if (pr1 * pr2 < 0) count++;
                //double cur = func(x);
                //if (!pr)
                //{
                //    if (prev * cur < 0)
                //    {
                //        count++;
                //    }
                //    else if (prev == 0 && cur != 0)
                //    {
                //        if (cur > 0)
                //        {
                //            if (x - 2 * h >= a)
                //            {
                //                double bef = func(x - 2 * h);
                //                if (bef < 0) count++;
                //            }
                //        }
                //        else if (cur < 0)
                //        {
                //            if (x - 2 * h >= a)
                //            {
                //                double bef = func(x - 2 * h);
                //                if (bef > 0) cur++;
                //            }
                //        }
                //    }
                //    else if (cur == 0 && prev != 0)
                //    {
                //        count++;
                //    }
                //}
                //prev = cur;
                //pr = false;
            }

            return count;
        }
        public void Task10(int[][] array, Action<int[][]> func)
        {

            // code here
            //Des(array, TotalReverse);
            //Des(array, SortBySumDesc);
            Des(array, func);
            // end

        }
        public delegate void Action(int[][] arr);
        public void Des(int[][] array, Action<int[][]> action)
        {
            action(array);
        }

        public void SortInCheckersOrder(int[][] array)
        {
            SortDelegate sortingDelegate;
            for (int i = 0; i < array.Length; i++)
            {
                int[] row = new int[array[i].Length];
                for (int j = 0;  j < array[i].Length; j++)
                {
                    row[j] = array[i][j];
                }
                sortingDelegate = (i % 2 == 0) ? SortAscending : SortDescending;
                sortingDelegate(row);
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = row[j];
                }
            }
        }
        public int Sum(int[] array)
        {
            int s = 0;
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i];
            }
            return s;
        }
        public void SortBySumDesc(int[][] array)
        {
            int[,] row = new int[array.Length, 2];
            int mx = 0;
            if (array.Length < 2) return;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    int sum1 = Sum(array[j]);
                    int sum2 = Sum(array[j + 1]);
                    if (sum1 < sum2)
                    {
                        int[] temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
        public void TotalReverse(int[][] array)
        {
            int left = 0;
            int right = array.Length - 1;
            while (left < right)
            {
                int[] temp = array[left];
                array[left] = array[right];
                array[right] = temp;
                left++;
                right--;
            }
            for (int i = 0; i < array.Length; i++)
            {
                left = 0;
                right = array[i].Length - 1;
                while (left < right)
                {
                    int temp = array[i][left];
                    array[i][left] = array[i][right];
                    array[i][right] = temp;
                    left++;
                    right--;
                }
            }
        }
        public int FindDiagonalMaxIndex(int[,] matrix)
        {
            int xm = 0;
            int ym = 0;
            int mx = matrix[0, 0];
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            var ans = new int[2];
            if (x == y)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, i] > mx)
                    {
                        mx = matrix[i, i];
                        ym = i;
                        xm = i;
                    }
                    if (matrix[i, x - i - 1] > mx)
                    {
                        mx = matrix[i, i];
                        ym = x - i - 1;
                        xm = i;
                    }
                }
            }
            ans[0] = xm;
            ans[1] = ym;
            return xm;
        }
        public void SwapRowColumn(int[,] matrix, int rowIndex, int[,] B, int columnIndex)
        {
            int xm = 0;
            int x1 = matrix.GetLength(0);
            int y1 = matrix.GetLength(1);
            int x2 = B.GetLength(0);
            int y2 = B.GetLength(1);
            if (x1 != x2 || x1 != y1 || y1 != y2)
            {
                return;
            }
            for (int j = 0; j < y1; j++)
            {
                int temp = matrix[rowIndex, j];
                matrix[rowIndex, j] = B[j, columnIndex];
                B[j, columnIndex] = temp;
            }
        }
        public int CountPositiveElementsInRow(int[,] matrix, int row) {
            int ind = 0;
            int mx = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[row, i] > 0)
                {
                    mx++;
                }
            }
            return mx;
        }
        public int CountPositiveElementsInColumn(int[,] matrix, int col)
        {
            int ind = 0;
            int mx = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, col] > 0)
                {
                    mx++;
                }
            }
            return mx;
        }
        public void InsertColumn(ref int[,] A, int rowIndex, int columnIndex, int[,] B)
        {
            int k = 0;
            int y1 = A.GetLength(1);
            int x2 = B.GetLength(0);
            if (x2 != y1)
            {
                return;
            }
            for (int i = 0; i < B.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    if (B[i, j] > 0) k++;
                }
            }
            int[,] ans = new int[A.GetLength(0) + 1, A.GetLength(1)];
            if (k > 0)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        if (i <= rowIndex) ans[i, j] = A[i, j];
                        else ans[i + 1, j] = A[i, j];
                    }
                }
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    ans[rowIndex + 1, j] = B[j, columnIndex];
                }
                A = ans;
            }
        }
        public void ChangeMatrixValues(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int elem = x * y;
            if (elem <= 5)
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        matrix[i, j] = matrix[i, j] * 2;
                    }
                }
                return;
            }
            int ind = 0;
            int[] val = new int[elem];
            int[] por = new int[elem];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    val[ind] = matrix[i, j];
                    por[ind] = ind;
                    ind++;
                }
            }
            for (int i = 0; i < elem; i++)
            {
                for (int j = 0; j < elem - i - 1; j++)
                {
                    bool swap = false;
                    if (val[j] < val[j + 1])
                    {
                        swap = true;
                    }
                    else if (val[j] == val[j + 1])
                    {
                        if (por[j] > por[j + 1])
                        {
                            swap = true;
                        }
                    }
                    if (swap)
                    {
                        int temp = val[j];
                        val[j] = val[j + 1];
                        val[j + 1] = temp;
                        int term = por[j];
                        por[j] = por[j + 1];
                        por[j + 1] = term;
                    }
                }
            }
            bool[] five = new bool[elem];
            for (int i = 0; i < 5; i++)
            {
                five[por[i]] = true;
            }
            ind = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (five[ind])
                    {
                        matrix[i, j] = matrix[i, j] * 2;
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j] / 2;
                    }
                    ind++; ;
                }
            }
            
        }
        public int[] CountNegativesPerRow(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int[] k = new int[x];

            for (int i = 0; i < x; i++)
            {
                int count = 0;
                for (int j = 0; j < y; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        count++;
                    }
                }
                k[i] = count;
            }
            return k;
        }
        public int FindMaxIndex(int[] array)
        {
            int ind = -1;
            int mx = int.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > mx && array[i] != 0)
                {
                    mx = array[i];
                    ind = i;
                }
            }
            return ind;
        }
        
        public delegate void SortRowsByMax(int[,] matrix);
        public void Help(int[,] matrix, SortRowsByMax sort)
        {
            sort(matrix);
        }
        public int[,] Clon(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            int[,] ans = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    ans[i,j] = matrix[i, j];
                }
            }
            return ans;
        }
        public int GetRowMax(int[,] matrix, int row)
        {
            int mx = matrix[row, 0];
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (matrix[row, j] > mx)
                {
                    mx = matrix[row, j];
                }
            }
            return mx;
        }
        public void SortRowsByMaxAscending(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(0) - 1; k++)
                {
                    int f = GetRowMax(matrix, k);
                    int s = GetRowMax(matrix, k + 1);
                    if (f > s)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            (matrix[k, j], matrix[k + 1, j]) = (matrix[k + 1, j], matrix[k, j]);
                        }
                    }
                }
            }
        }
        public void SortRowsByMaxDescending(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(0) - 1; k++)
                {
                    int f = GetRowMax(matrix, k);
                    int s = GetRowMax(matrix, k + 1);
                    if (f < s)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            (matrix[k, j], matrix[k + 1, j]) = (matrix[k + 1, j], matrix[k, j]);
                        }
                    }
                }
            }
        }
        
        
        public delegate void SortDelegate(int[] arr);
        public static void SortAscending(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
        public static void SortDescending(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
        
        
        public int[] Clone(int[] array)
        {
            int x = array.Length;
            int[] ans = new int[x];
            for (int i = 0; i < x; i++)
            {
                ans[i] = array[i];
            }
            return ans;
        }
    }
}