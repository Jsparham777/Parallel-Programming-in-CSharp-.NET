using System.Threading;

namespace ParallelProgramming
{
    /// <summary>
    /// Interlocked Operations (Section 2 - 11)
    /// </summary>
    public class BankAccount2
    {
        private int _balance;

        public int Balance { get => _balance; private set => _balance = value; }

        public void Deposit(int amount)
        {
            //Deposit instructions (not atomic, i.e more than one instruction)

            //+= (two instructions)

            // op1: temp <- get_Balance() + amount
            // op2: set_Balance(temp)

            Interlocked.Add(ref _balance, amount);
        }

        public void Withdraw(int amount)
        {
            //Withdraw instructions (not atomic, i.e more than one instruction)

            //-= (two instructions)

            // op1: temp <- get_Balance() - amount
            // op2: set_Balance(temp)

            Interlocked.Add(ref _balance, -amount);
        }
    }
}
