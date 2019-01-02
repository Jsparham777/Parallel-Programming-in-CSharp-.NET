namespace ParallelProgramming
{
    /// <summary>
    /// Critical Sections (Section 2 - 10)
    /// </summary>
    public class BankAccount1
    {
        /// <summary>
        /// Locks the instruction
        /// </summary>
        public object padlock = new object();

        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            //Deposit instructions (not atomic, i.e more than one instruction)

            //+= (two instructions)

            // op1: temp <- get_Balance() + amount
            // op2: set_Balance(temp)

            //Lock the Balance for modification
            lock (padlock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            //Withdraw instructions (not atomic, i.e more than one instruction)

            //-= (two instructions)

            // op1: temp <- get_Balance() - amount
            // op2: set_Balance(temp)

            //Lock the Balance for modification
            lock (padlock)
            {
                Balance -= amount;
            }
        }
    }
}
