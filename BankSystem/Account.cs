using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankSystem {
    public class Account {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        private List<Transaction> _transactions = new List<Transaction>();

        public Account(string id) {
            Id = id;
            Balance = 0;
        }

        public List<Transaction> Transactions {
            get {
                return _transactions;
            }
            set {
                _transactions = value;
            }
        }

        public void CreateAccount(Bank bank, string firstname, string lastname) {
            User customer = new User(firstname, lastname, bank.Name);
            bank.Users.Add(customer);
        }

        public Account GetAccountById(Bank bank, string id) {
            return bank.Accounts.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public bool DeleteAccount(Bank bank) {
          
            User customer = bank.Users.Where(c => c.AccountId.Equals(this.Id)).FirstOrDefault();
            bank.Users.Remove(customer);
            return true;
        }

        public bool DepositMoney(decimal amt) {
            this.Balance += amt;
            return true;
        }

        public decimal WithdrawMoney(decimal amt) {
            if (this.Balance >= amt) {
                this.Balance -= amt;
                return amt;
            }
            return 0;
        }

        public void UpdateAccount(Bank bank, string toUpdate, int option) {
            User customer = bank.Users.Where(u => u.AccountId.Equals(Id) && !u.IsEmployee).FirstOrDefault();

            if (option == (int)Update.firstname) {
                customer.Firstname = toUpdate;
            }
            else if (option == (int)Update.lastname) {
                customer.Lastname = toUpdate;
            }
            else if (option == (int)Update.username) {
                customer.Username = toUpdate;
            }
            else if (option == (int)Update.password) {
                customer.Password = toUpdate;
            }
            else {
                return;
            }
        }

        public bool AcceptNewCurrency(decimal amt) {
            this.Balance += amt;
            return true;
        }
    }
}
