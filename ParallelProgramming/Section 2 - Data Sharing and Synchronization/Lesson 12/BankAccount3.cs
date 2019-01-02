namespace ParallelProgramming
{
    /// <summary>
    /// Spin Locking and Lock Recursion (Section 2 - 12)
    /// </summary>
    public class BankAccount3
    {
        private int _balance;

        public int Balance { get => _balance; private set => _balance = value; }

        public void Deposit(int amount)
        {
            _balance += amount;
        }

        public void Withdraw(int amount)
        {
            _balance -= amount;
        }
    }
}
