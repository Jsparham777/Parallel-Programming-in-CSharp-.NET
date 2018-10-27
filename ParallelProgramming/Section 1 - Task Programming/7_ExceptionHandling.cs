using System;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Section 1 - 7
    /// </summary>
    internal static class ExceptionHandling
    {
        public static void Start()
        {
            try
            {
                //Run the Tasks
                Test();
            }
            catch (AggregateException ae)
            {
                //Handle any remaining exceptions
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine($"Handled elsewhere: {e.GetType()}");
            }            
        }

        private static void Test()
        {
            //Start a Task which throws an invalid operation exception
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t1" };
            });

            //Start a Task which throws an access violation exception
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });


            try
            {
                //Wait for all tasks to finish
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                //Only catch invalid operation exceptions (thrown by t1)
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid operation!");
                        return true;
                    }
                    else return false;
                });
            }
        }
    }
}
