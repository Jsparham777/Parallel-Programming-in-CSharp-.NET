using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Spin Locking and Lock Recursion (Section 2 - 12)
    /// </summary>
    internal static class SpinLockingAndLockRecursion
    {
        /// <summary>
        /// Spin lock
        /// </summary>
        public static void Start()
        {
            var tasks = new List<Task>();

            var ba = new BankAccount3();

            //Spins the thread without yielding (giving up the thread slot) until you are ready to execute
            var sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                //Deposit
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //Initialise the lock taken flag
                        var lockTaken = false;
                        try
                        {
                            //Aquire the lock and deposit the money
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            //Release the lock
                            if (lockTaken) sl.Exit();
                        }
                    }
                }));

                //Withdraw
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //Initialise the lock taken flag
                        var lockTaken = false;
                        try
                        {
                            //Aquire the lock and deposit the money
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            //Release the lock
                            if (lockTaken) sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            //Different balnce each time as the Withdraw and Deposit methods are not atmoic
            Console.WriteLine($"Final balance is {ba.Balance}.");
        }

        /// <summary>
        /// Lock Recursion
        /// </summary>
        public static void Start2()
        {
            LockRecursion(5);
        }


        private static SpinLock slRecursion = new SpinLock(true);

        private static void LockRecursion(int x)
        {
            bool lockTaken = false;

            try
            {
                slRecursion.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursion(x - 1);
                    slRecursion.Exit();
                }
                else
                    Console.WriteLine($"Failed to take a lock, x = {x}");
            }
        }
    }
}
