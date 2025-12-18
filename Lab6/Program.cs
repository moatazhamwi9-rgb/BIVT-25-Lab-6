using System.Globalization;

namespace Lab6
{
    public class Program
    {
        public static void Main()
        {
            Purple purple = new Purple();

            int[] arr = new int[6] { 1, 0, -4, 4, -1, -10 };
            purple.SortNegativeDescending(arr);
            Console.WriteLine(string.Join(" ", arr));
            
             arr = new int[6] { 1, 0, -4, 4, -1, -10 };
            purple.SortNegativeAscending(arr);
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
