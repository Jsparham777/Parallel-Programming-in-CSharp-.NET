using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Section 1 - 5
    /// </summary>
    internal static class WaitingForTasks
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token; //Get the token

            var t1 = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    //Throw an exception if cancellation token is requested, otherwise sleep
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done");

            }, token); //Pass the token
            t1.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //Wait for task 1
            //t.Wait();

            //Wait for any task to finish
            //Task.WaitAny(t1, t2);

            //Wait for any task to finish (using an array of tasks), applying a timeout of 4 seconds
            Task.WaitAny(new[] { t1, t2 }, 4000, token);

            Console.WriteLine($"Task t1 status is {t1.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            Console.ReadKey();
            cts.Cancel();
        }
    }
}
