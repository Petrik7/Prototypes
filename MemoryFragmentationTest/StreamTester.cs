//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;
//using System.Timers;
//using System.IO;

//namespace TestOutOfMemory
//{
//    class StreamTester
//    {
//        public const int MB = 1024 * 1024;
//        public const int ArraySize = 50 * MB;


//        byte[] _allocatedMemory;
//        byte[] _allocatedMemory_1;

//        public StreamTester()
//        {
//            _allocatedMemory = new byte[512 * ByteTester.MB];
//            _allocatedMemory[1] = 1;
//            //_allocatedMemory_1 = new byte[ArraySize*30];
//            //_allocatedMemory_1[1] = 1;

//            //byte[] allocatedMemory = new byte[memoryToPreallocate];
//            //allocatedMemory[1] = 1;
//            //byte[] allocatedMemory_1 = new byte[ArraySize * 30];
//            //allocatedMemory_1[1] = 1;
//        }

//        public void RunTimer()
//        {
//            Timer timer = new Timer();
//            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
//            timer.Interval = 10;
//            timer.Enabled = true;
//        }

//        private static void OnTimedEvent(object source, ElapsedEventArgs e)
//        {
//            lock (rwlock)
//            {
//                //smallArray = null;
//                //s_collectionOfSmall.Clear();
//            }

//            ByteTester.RunBytes();
//        }

//        private static object rwlock = new object();
//        private static byte[] somedata = new byte[];
//        private static int counter = 1;
//        private static List<byte[]> s_collectionOfSmall = new List<byte[]>();

//        public static void RunStreams()
//        {
//            int i = MB;
//            try
//            {
//                lock (rwlock)
//                {
//                    Debug.Print(string.Format("Counter = {0}", counter));
//                    ++counter;
//                }

//                for (; i < MB + 200; i += 10)
//                {
//                    byte[] marshaled = Marshal(i);
//                    marshaled[1] = 1;
//                    marshaled[2] = (byte)(marshaled[1] + 1);

//                    //byte[] smallArray = new byte[2 * MB]; // Ok

//                    byte[] encripted = Encript(marshaled, i);
//                    encripted[1] = 1;
//                    encripted[2] = (byte)(encripted[1] + 1);
//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.Print(ex.Message);
//            }
//        }


//        private static MemoryStream Marshal(int i)
//        {
//            MemoryStream ms = new MemoryStream();
//            ms.Write(
//            return ms;
//        }

//        private static byte[] Encript(byte[] marshaled, int i)
//        { 
//            return new byte[ArraySize + i];
//        }
//    }
//}
