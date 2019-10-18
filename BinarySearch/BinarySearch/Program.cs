using System;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] { 1, 5, 0, 34,1415,23,46,69 };
            Array.Sort(numbers);
            var index = LinearSearch(numbers, 69);
            //Console.WriteLine(index);
            var index2 = BinarySearch(numbers, 69);
        
            var index3 = BinaryRecursiveSearch(numbers,69);
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Index2 : {index2}, Index3 : {index3}");
            Console.ReadLine();
        }
        private static int LinearSearch(int[] array,int item)
        {
            for(int i =0; i<array.Length; i++)
            {
                Console.WriteLine(i);
                if (array[i] == item)
                { return i; }

            }
            return -1;
        }
        private static int BinarySearch(int[] array,int item)
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("BINARY SEARCH");
            int left = 0;
            int right = array.Length - 1;
            while(left <= right)
            {
                var middle = (left + right) / 2;
                Console.WriteLine($"Middle : {middle}, Right : {right}, Left : {left}");
                if (array[middle] == item)
                    return middle;
                if (item < array[middle])
                    right = middle - 1;
                else
                    left = middle + 1;
            }
            return -1;
        }

        private static int BinaryRecursiveSearch(int[] array,int item)
        {
            return BinaryRecursiveSearch(array, 0, array.Length, item);
        }
        private static int BinaryRecursiveSearch(int[] array,int left,int right, int item)
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("BINARY RECURSIVE SEARCH");

            if (left <= right)
            {
                var middle = (left + right) / 2;
                Console.WriteLine($"Middle : {middle}, Right : {right}, Left : {left}");
                if (array[middle] == item)
                    return middle;
                if (item < array[middle])
                   return BinaryRecursiveSearch(array, left, middle-1, item);
                else
                    return BinaryRecursiveSearch(array, middle+1,right, item);
            }
            return -1;
        }
    }
}
