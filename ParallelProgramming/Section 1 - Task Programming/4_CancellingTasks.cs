using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Section 1 - 4
    /// </summary>
    internal static class CancellingTasks
    {
        public static void Start()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            //Register some code to fire when cancellation has been requested
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has be requested.");
            });

            //Start the task
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //Throw OperationCancelledException if cancellation requested
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token); //Provide token

            //Start the task
            t.Start();

            //Start a task 
            Task.Factory.StartNew(() =>
            {
                //When the token is cancelled, fire this code
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been released, cancellation was requested.");
            });

            Console.ReadKey();
            cts.Cancel();



            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            //Link the cancellation tokens into one token
            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(1000);
                }
            }, paranoid.Token); //Provide token

            Console.ReadKey();
            emergency.Cancel(); //Will cause paranoid to cancel
        }
    }
}
