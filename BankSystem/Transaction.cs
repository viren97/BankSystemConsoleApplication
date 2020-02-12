using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem {
    public enum TXNType{
        debit = 1,
        credit = 2
    }
    public class Transaction {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Datetime { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }


        public Transaction(string id, decimal amt, string type, string senderId, string receiverId) {
            Id = id;
            Amount = amt;
            Type = type;
            SenderId = senderId;
            ReceiverId = receiverId;
            Datetime = DateTime.Now.ToString();

        }

        public Transaction() {

        }

        public string TransferMoneyThroughRTGS(Bank bank, Account sender, Account receiver, decimal amt) {
            bool isReceiverBankSame = bank.Accounts.Contains(receiver);

            if (sender.Balance < amt) {
                return null;
            }
            sender.Balance -= amt;
            string id = "TXN" + bank.Id + sender.Id + DateTime.Now.ToString("dd/MM/yyyy");
            if (isReceiverBankSame) {
                amt -= (bank.ServiceCharges.SameBankRTGS * amt) / 100;
                receiver.Balance += amt;
                this.Id = id;
                this.Amount = amt;
                this.Type = TXNType.debit.ToString();
                this.SenderId = sender.Id;
                this.ReceiverId = receiver.Id;
                Transaction receivertxn = new Transaction(id, amt, TXNType.credit.ToString(), sender.Id, receiver.Id);
                receiver.Transactions.Add(receivertxn);


            }
            else {
                amt -= (bank.ServiceCharges.OtherBankRTGS * amt) / 100;
                receiver.Balance += amt;

                this.Id = id;
                this.Amount = amt;
                this.Type = TXNType.debit.ToString();
                this.SenderId = sender.Id;
                this.ReceiverId = receiver.Id;
                this.Datetime = DateTime.Now.ToString();
                Transaction receivertxn = new Transaction(id, amt, TXNType.credit.ToString(), sender.Id, receiver.Id);
                receiver.Transactions.Add(receivertxn);

            }
            return id;
        }

        public string TransferMoneyThroughIMPS(Bank bank, Account sender, Account receiver, decimal amt) {
            bool isReceiverBankSame = bank.Accounts.Contains(receiver);

            if (sender.Balance < amt) {
                return null;
            }
            sender.Balance -= amt;
            string id = "TXN" + bank.Id + sender.Id + DateTime.Now.ToString("dd/MM/yyyy");
            if (isReceiverBankSame) {
                amt -= (bank.ServiceCharges.SameBankIMPS * amt) / 100;
                receiver.Balance += amt;
                this.Id = id;
                this.Amount = amt;
                this.Type = TXNType.debit.ToString();
                this.SenderId = sender.Id;
                this.ReceiverId = receiver.Id;
                this.Datetime = DateTime.Now.ToString();
                Transaction receivertxn = new Transaction(id, amt, TXNType.credit.ToString(), sender.Id, receiver.Id);
                receiver.Transactions.Add(receivertxn);
            }
            else {
                amt -= (bank.ServiceCharges.OtherBankIMPS * amt) / 100;
                receiver.Balance += amt;

                this.Id = id;
                this.Amount = amt;
                this.Type = TXNType.debit.ToString();
                this.SenderId = sender.Id;
                this.ReceiverId = receiver.Id;
                this.Datetime = DateTime.Now.ToString();
                Transaction receivertxn = new Transaction(id, amt, TXNType.credit.ToString(), sender.Id, receiver.Id);
                receiver.Transactions.Add(receivertxn);
            }
            return id;
        }

        public string RevertTransaction(Bank bank, Account sender, Account receiver) {
            Transaction newTransaction = new Transaction();
            sender.Transactions.Add(newTransaction);
            return newTransaction.TransferMoneyThroughRTGS(bank, sender, receiver, Amount);
        }
    }
}
