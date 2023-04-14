using System;
using System.Threading;

class Program
{
    static int[] arr;
    static object lockObject = new object();

    static void Main(string[] args)
    {
        int size = 100;
        arr = new int[size];
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = random.Next(10);
        }
        ParallelQuickSort(0, size - 1);
        for (int i = 0; i < size; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }

    static void ParallelQuickSort(int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(left, right);
            Thread leftThread = new Thread(() => ParallelQuickSort(left, pivot - 1));
            Thread rightThread = new Thread(() => ParallelQuickSort(pivot + 1, right));
            leftThread.Start();
            rightThread.Start();
            leftThread.Join();
            rightThread.Join();
        }
    }
    static int Partition(int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Swap(i, j);
            }
        }

        Swap(i + 1, right);

        return i + 1;
    }
    static void Swap(int i, int j)
    {
        lock (lockObject)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}