using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Account : IAccount
    {
        protected const int PRECISION = 4;

        public string Name { get; }
        public decimal Balance { get; private set; }


        public bool IsBlocked { get; private set; } = false;
        public void Block() => IsBlocked = true;
        public void Unblock() => IsBlocked = false;

        public Account(string name, decimal initialBalance = 0)
        {
            if (name == null || initialBalance < 0)
                throw new ArgumentOutOfRangeException();
            Name = name.Trim();
            if (Name.Length < 3)
                throw new ArgumentException();
            Balance = Math.Round(initialBalance, PRECISION);
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0 || IsBlocked) return false;

            Balance = Math.Round(Balance += amount, PRECISION);
            return true;
        }

        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || amount > Balance) return false;

            Balance = Math.Round(Balance -= amount, PRECISION);
            return true;
        }

        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked"
                        : $"Account name: {Name}, balance: {Balance:F2}";
    }








    internal class AccountPlus : Account, IAccountWithLimit
    {
        private decimal debt = 0;

        public AccountPlus(string name, decimal initialLimit = 100, decimal initialBalance = 0) : base(name)
        {
            if (name == null) { throw new ArgumentOutOfRangeException("Name is null"); }

            Name = name.Trim();

            if (Name.Length < 3) { throw new ArgumentException(); }

            Balance = Math.Round(initialBalance, 4);
            if (Balance < 0) { throw new ArgumentOutOfRangeException(); }

            if (initialLimit < 0)
            {
                OneTimeDebetLimit = 0;
            }
            else
            {
                OneTimeDebetLimit = initialLimit;
            }

            AvaibleFounds = Balance + OneTimeDebetLimit; 
        }

        new public string Name
        {
            get;
        }


        private decimal oneTimeDebetLimit;
        public decimal OneTimeDebetLimit 
        {
            get
            {
                return oneTimeDebetLimit;
            }
            set 
            {  // value = 500, is blocked = true
                if(IsBlocked == false && value > 0)
                {
                    AvaibleFounds = AvaibleFounds + value - oneTimeDebetLimit;

                    oneTimeDebetLimit = value;
                }
            } 
        }

        new public decimal Balance { get; private set; }

        public decimal AvaibleFounds { get; private set; }

        new public bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || amount > AvaibleFounds) return false;
            else if(amount < Balance) 
            { 
                Balance = Math.Round(Balance -= amount, PRECISION);
                AvaibleFounds -= amount;
                return true;
            }
            else if(amount < AvaibleFounds)
            {
                debt = amount - Balance; // przypisuje jej wartosc wypłata_z_konta - balans, czyli tyle ile należy potem oddać
                Balance = 0;
                AvaibleFounds -= amount;
                Block();
                return true;
            }
            return false;
        }

        new public bool Deposit(decimal amount)
        {
            if (amount <= 0 || (IsBlocked && debt == 0)) return false;

            if(debt < amount && debt != 0)
            {
                Balance += amount - debt;
                debt = 0;             
                AvaibleFounds += amount;
                Unblock();
                return true;
            }
            else if(debt > amount)
            {
                debt -= amount;
                return true;
            }
            else if(debt == amount)
            {
                debt = 0;
                Unblock();
                return true;
            }
            else
            {
                Balance = Math.Round(Balance += amount, PRECISION);
                return true;
            }  
        }

        

        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}"
                        : $"Account name: {Name}, balance: {Balance:F2}, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}";
    }
}
