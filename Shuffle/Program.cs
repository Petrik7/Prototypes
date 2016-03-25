using System;
using System.Text;

namespace Shuffle
{
    class Program
    {
        static void TestShuffle()
        {
            ShuffleNumbers shuffler = new ShuffleNumbers();
            const int n = 8;
            int[] res1 = shuffler.Shuffle(n);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            int[] res2 = shuffler.Shuffle(n);

            System.Diagnostics.Debug.Assert(n == res1.Length);
            System.Diagnostics.Debug.Assert(n == res2.Length);

            int diff = 0;
            for (int i = 0; i < res1.Length; ++i)
            {
                if (res1[i] != res2[i])
                    ++diff;
            }
            System.Diagnostics.Debug.Assert(diff >= n/2);

            Array.Sort(res1);
            for (int i = 0; i < res1.Length; ++i)
            {
                System.Diagnostics.Debug.Assert(res1[i] == (i + 1));
            }
        }

        static void Main(string[] args)
        {
            //TestShuffle();

            int n = 0;
            string nStr = string.Empty;
            bool commandLineParam = (args.Length > 0);
            ShuffleNumbers shuffler = new ShuffleNumbers();

            if (!commandLineParam)
            {
                Console.Error.WriteLine("In interactive mode just type N below or start it with N as a command line parameter, Enjoy!");
            }

            do
            {
                if(commandLineParam)
                {
                    nStr = args[0];
                }
                else
                {
                    Console.WriteLine("Please enter integer N");
                    nStr = Console.ReadLine();
                }

                if (int.TryParse(nStr, out n))
                {
                    try
                    {
                        OutputNumbers(shuffler.Shuffle(n));
                    }
                    catch (OutOfMemoryException)
                    {
                        Console.Error.WriteLine("Not enough memory to handle your request");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Input is not an integer.");
                }
                
            }while (!commandLineParam);
        }

        static private void OutputNumbers(int[] numbers)
        {
            StringBuilder result = new StringBuilder();
            foreach (int number in numbers)
            {
                result.Append(number);
                result.Append(Environment.NewLine);
            }
            Console.Write(result.ToString());
        }
    }
}
