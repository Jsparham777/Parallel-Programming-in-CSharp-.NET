using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Interlocked Operations (Section 2 - 11)
    /// </summary>
    internal static class InterlockedOperations
    {
        public static void Start()
        {
            var tasks = new List<Task>();
            
            var ba = new BankAccount2();

            for (int i = 0; i < 10; i++)
            {
                //Deposit
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                //Withdraw
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));                
            }

            Task.WaitAll(tasks.ToArray());

            //Different balnce each time as the Withdraw and Deposit methods are not atmoic
            Console.WriteLine($"Final balance is {ba.Balance}.");
        }
    }
}
