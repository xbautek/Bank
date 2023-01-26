using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Account : IAccount
    {

        private bool isBlocked;
        public decimal balance;


        public Account(string name, decimal balance = 0, bool isBlocked = false) 
        {
            if(name == null) { throw new ArgumentOutOfRangeException("Name is null"); }

            Name = name.Trim();

            if (Name.Length < 3 ) { throw new ArgumentException(); }

            Balance = Math.Round(balance,4);
            if (Balance < 0) { throw new ArgumentOutOfRangeException(); }
        }


        public string Name 
        {
            get;   
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public bool IsBlocked
        {
            get
            {
                return isBlocked;
            }
            set
            {
                isBlocked = value;
            }
        }

        public void Block()
        {
            isBlocked = true;
        }

        public void Unblock()
        {
            isBlocked = false;
        }

        public bool Deposit(decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }
            else if(isBlocked == true)
            {
                return false;
            }
            else
            {
                balance += amount;
                return true;
            }
        }

        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            else if (isBlocked == true)
            {
                return false;
            }
            else if (Balance < amount)
            {
                return false;
            }
            else
            {
                balance -= amount;
                return true;
            }
        }

        public override string ToString()
        {

            if (isBlocked)
            {
                return $"Account name: {Name}, balance: {Balance.ToString("F",CultureInfo.InvariantCulture)}, blocked";
            }
            else
            {
                return $"Account name: {Name}, balance: {Balance.ToString("F", CultureInfo.InvariantCulture)}";
            }
        }
    }
}
