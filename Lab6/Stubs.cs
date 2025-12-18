using System;

namespace Lab6
{
    // Вспомогательные заглушки для отсутствующих типов других лиг.
    public delegate void Sorting(Array array);
    public delegate void SortRowsByMax(int[,] matrix);
    public delegate int[] FindNegatives(int[,] matrix);

    public class MathInfo
    {
    }

    public delegate double BikeRide(double v, double a);
}
