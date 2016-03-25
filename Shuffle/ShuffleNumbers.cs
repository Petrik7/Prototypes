using System;
using System.Text;
using System.Diagnostics;

namespace Shuffle
{
    class ShuffleNumbers
    {
        public int[] Shuffle(int n)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int[] numbers = new int[n];
                        
            for (int i = 0; i < n; ++i)
            {
                numbers[i] = i + 1;    
            }

            Random randomGen = new Random();
            for (int i = 0; i < n - 1; ++i)
            {
                int j = randomGen.Next(i, n);
                //swap intentionally inlined
                int tmp = numbers[j];
                numbers[j] = numbers[i];
                numbers[i] = tmp;
            }

            stopWatch.Stop();
            Console.Error.WriteLine("Shuffling of {0} numbers took {1} ms",  n, stopWatch.Elapsed.TotalMilliseconds);
            return numbers;
        }
    }
}
