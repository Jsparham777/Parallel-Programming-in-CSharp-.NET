namespace ParallelProgramming
{
    /// <summary>
    /// Mutex (Section 2 - 13)
    /// </summary>
    public class BankAccount4
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

        public void Transfer(BankAccount4 where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }
}
