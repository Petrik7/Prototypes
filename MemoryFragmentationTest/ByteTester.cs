using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace TestOutOfMemory
{
    class ByteTester
    {
        public const int KB = 1024;
        public const int MB = KB * KB;
        public const int ArraySize = 1*MB;


        byte[] _allocatedMemory;
        byte[] _allocatedMemory_1;

        public ByteTester()
        {
            _allocatedMemory = new byte[600 * MB];
            _allocatedMemory[1] = 1;
            _allocatedMemory_1 = new byte[400 * MB];
            _allocatedMemory_1[1] = 1;

            //byte[] allocatedMemory = new byte[memoryToPreallocate];
            //allocatedMemory[1] = 1;
            //byte[] allocatedMemory_1 = new byte[ArraySize * 30];
            //allocatedMemory_1[1] = 1;
        }

        public void RunTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 40;
            //timer.Enabled = true;

            RunBytes();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lock (rwlock)
            {
                //smallArray = null;
                //s_collectionOfSmall.Clear();
            }

            ByteTester.RunBytes();
        }

        private static object rwlock = new object();
        private static byte[] smallArray = null;
        private static int counter = 1; //38 17
        private static List<byte[]> s_collectionOfSmall = new List<byte[]>();

        public static void RunBytes()
        {
            int i = MB;
            int allocationSize = 500*KB;
            Random random = new Random();
            int adder = KB;// random.Next(800, 5000);
           
            try
            {
                long ticksThisTime = 0;
                long ticksMax = 0;
                Stopwatch timePerParse;
                allocationSize = ArraySize + adder;

                //for (; i < MB + 10000000; i += 10000) //1000 23000
                //for (; i < MB + 5000000; i += KB) //1000 230,000 allocations  170000
                for (int k = 0; k < 1000000000; ++k )
                {
                    //allocationSize = random.Next(500 * KB, 9 * MB);

                    //Debug.Print(string.Format("allocationSize = {0}", allocationSize));
                    timePerParse = Stopwatch.StartNew();

                    byte[] marshaled = Marshal(allocationSize);
                    marshaled[1] = 1;
                    marshaled[2] = (byte)(marshaled[1] + 1);

                    //byte[] smallArray = new byte[2 * MB]; // Ok

                    byte[] encripted = Encript(marshaled, allocationSize + 10);
                    encripted[1] = 1;
                    encripted[2] = (byte)(encripted[1] + 1);

                    timePerParse.Stop();
                    ticksThisTime = timePerParse.ElapsedTicks;
                    ticksMax = Math.Max(ticksThisTime, ticksThisTime);
                    lock (rwlock)
                    {
                        ++counter;
                    }
                    allocationSize += 10 * KB;
                    Debug.Print(string.Format("+++ Counter = {0} ticksMax = {1} allocationSize = {2}", counter, ticksMax, allocationSize));
                }
                
                lock (rwlock)
                {
                    Debug.Print(string.Format("+++ Counter = {0} ticksMax = {1}", counter, ticksMax));
                    //++counter;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(string.Format("+++ Counter = {0}", counter));
                Debug.Print(ex.Message);
            }
        }

        private static byte[] Marshal(int size)
        {
            return new byte[size];
        }

        private static byte[] Encript(byte[] marshaled, int size)
        {
            return new byte[size];
        }

    }
}
