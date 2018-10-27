using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Section 1 - 6
    /// </summary>
    internal static class WaitingForTimeToPass
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token; //Get the token

            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm; you have 5 seconds");

                //Delay 5 seconds before cancellation is raised
                bool cancelled = token.WaitHandle.WaitOne(5000); 

                Console.WriteLine(cancelled ? "Bomb disarmed" : "BOOM!");

            }, token); //Pass the token

            t.Start();

            Console.ReadKey();
            cts.Cancel();
        }
    }
}
