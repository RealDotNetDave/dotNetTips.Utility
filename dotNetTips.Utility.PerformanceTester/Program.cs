using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using dotNetTips.Utility.Standard.Collections.Generic.Concurrent;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.Xml;

namespace dotNetTips.Utility.PerformanceTester
{
    class Program
    {
        static int TestRunCount = 2;
        static int TestRunCollectionCount = 10000;

        static void Main()
        {
            //Warm up
            RunConcurrentHashSet();
           RunConcurrentDictionary();

            var stopTime = DateTime.Now.AddMinutes(5);

            Console.Clear();

            TestDisposeMethods();

            var generator = new Random();

            do
            {
                TestRunCollectionCount = generator.Next(100, 5000);
                TestRunCount = generator.Next(2, 100);

                RunConcurrentDictionary();
                RunConcurrentHashSet();

            } while (DateTime.Now < stopTime);

            TestDisposeMethods();

            Console.Beep();
            Console.WriteLine("DONE");
            Console.ReadLine();

        }

        private static void TestDisposeMethods()
        {
            var testCollection = new ConcurrentDictionary<decimal, DataSet>();
            
            for (int i = 0; i < 10000; i++)
            {
                var item = new KeyValuePair<decimal, DataSet>(i, new DataSet(i.ToString()));

                testCollection.AddIfNotExists(item);
            }

            testCollection.DisposeCollection();

            
        }

        static void RunConcurrentHashSet()
        {
            var threads = new List<Task>();
            var sw = new Stopwatch();

            var ch = new ConcurrentHashSet<DataTable>();

                sw.Start();

                for (int runCount = 0; runCount < TestRunCount; runCount++)
                {
                    threads.Add(Task.Factory.StartNew(() => ProcessConcurrentHashSet(ch, TestRunCollectionCount)));
                }

                Task.WaitAll(threads.ToArray());

                sw.Stop();

                //Test XML
                //var xml = XmlHelper.Serialize(ch);

                //var xmlResult = XmlHelper.Deserialize<ConcurrentHashSet<DataTable>>(xml);

                //var json = ch.ToJson();

                //var jsonResult = json.FromJson<ConcurrentHashSet<DataTable>>();

                var count = ch.Count();
                double avg = sw.ElapsedMilliseconds / count;

                Console.WriteLine("ConcurrentHasSet test took {0} milliseconds, Count={1}-{2}.", sw.ElapsedMilliseconds, count, avg);


            Console.WriteLine();
        }

        static void RunConcurrentDictionary()
        {
            var threads = new List<Task>();
            var sw = new Stopwatch();

            var ch = new DistinctConcurrentBag<Guid>();

            sw.Start();

            for (int runCount = 0; runCount < TestRunCount; runCount++)
            {
                threads.Add(Task.Factory.StartNew(() => ProcessConcurrentDictionary(ch, TestRunCollectionCount)));
            }

            Task.WaitAll(threads.ToArray());

            sw.Stop();


            //var test = XmlHelper.Serialize(ch);

            //var testResult = XmlHelper.Deserialize<DistinctConcurrentBag<Guid>>(test);

            //var json = ch.ToJson();

            //var jsonResult = json.FromJson<DistinctConcurrentBag<Guid>>();


            var count = ch.Count;
            double avg = sw.ElapsedMilliseconds / count;

            Console.WriteLine("CB test took {0} milliseconds, Count={1}-{2}.", sw.ElapsedMilliseconds, count, avg);
            Console.WriteLine();
        }


        private static void ProcessConcurrentHashSet(ConcurrentHashSet<DataTable> ch, int collectionCount)
        {
            Console.WriteLine("CHS Collection Count={0}...", collectionCount);

            for (var iterationCount = 0; iterationCount < collectionCount; iterationCount++)
            {
                ch.Add(new DataTable(iterationCount.ToString()));
            }
        }

        private static void ProcessConcurrentDictionary(DistinctConcurrentBag<Guid> ch, int collectionCount)
        {
            Console.WriteLine("DCB Collection Count={0}...", collectionCount);

            for (var iterationCount = 0; iterationCount < collectionCount; iterationCount++)
            {
                ch.Add(Guid.NewGuid());
            }
        }

    }
}
