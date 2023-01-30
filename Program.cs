namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // scenariusz: wpłaty wypłaty, blokada konta
            // utworzenie konta plus z domyslnym limitem 100
            var john = new AccountPlus("John", initialBalance: 100.0m);
            Console.WriteLine(john);

            // wypłata - podanie kwoty ujemnej
            john.Withdrawal(-50.0m);
            Console.WriteLine(john);

            // wypłata bez wykorzystania debetu
            john.Withdrawal(50.0m);
            Console.WriteLine(john);

            // wypłata z wykorzystaniem debetu
            john.Withdrawal(100.0m);
            Console.WriteLine(john);

            // konto zablokowane, wypłata niemożliwa
            john.Withdrawal(10.0m);
            Console.WriteLine(john);

            // wpłata odblokowująca konto
            john.Deposit(80.0m);
            Console.WriteLine(john);

            // wpłata podanie kwoty ujemnej
            john.Deposit(-80.0m);
            Console.WriteLine(john);
        }
    }
}