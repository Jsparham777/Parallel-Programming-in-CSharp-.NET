using System;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    /// <summary>
    /// Section 1 - 3
    /// </summary>
    internal static class CreatingAndStartingTasks
    {
        public static void Start()
        {
            //Start a new Task, executing 'Write' method automatically
            Task.Factory.StartNew(() => Write('.'));

            //Create a task to execute 'Write', then start it (using a lambda expression)
            var t1 = new Task(() => Write('.'));
            t1.Start();

            //Create a task to execute 'Write', then start it (passing the parameter as a state)
            Task t2 = new Task(Write, "hello");
            t2.Start();

            //Start a new Task, executing 'Write' method automatically (passing the parameter as s state)
            Task.Factory.StartNew(Write, 123);

            //Starting a task with a method of return type integer
            string text1 = "testing", text2 = "";
            var t3 = new Task<int>(TextLength, text1);
            t3.Start();

            //Start a task automatically, executing a method of return type int
            Task<int> t4 = Task.Factory.StartNew(TextLength, text2);

            //Requesting the result of a Task is a thread blocking command
            Console.WriteLine($"Length of {text1} is {t3.Result}");
            Console.WriteLine($"Length of {text2} is {t4.Result}");
        }

        private static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        private static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
    }
}
