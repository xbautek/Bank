namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var account = new Account("John");
            account.Deposit(100.00m);
            Console.WriteLine(account.Withdrawal(10.00m));
            Console.WriteLine(account);
            Console.WriteLine(account.Withdrawal(100.00m));
            Console.WriteLine(account);
            Console.WriteLine(account.Withdrawal(0.00m));
            Console.WriteLine(account);
            Console.WriteLine(account.Withdrawal(-10.00m));
            Console.WriteLine(account);
            account.Block();
            Console.WriteLine(account.Withdrawal(10.4999m));
            Console.WriteLine(account);
        }
    }
}