using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem {
    public enum LoginAs {
        Employee = 1,
        Customer = 2
    }

    public enum Update {
        firstname = 1,
        lastname = 2,
        username = 3,
        password = 4
    }

    public enum UpdateServiceChargeOf {
        sameBankRTGS = 1,
        sameBankIMPS = 2,
        otherBankRTGS = 3,
        otherBankIMPS = 4
    }

    public enum EmployeeOption {
        ViewAllAccounts = 1,
        CreateAccounts = 2,
        UpdateAccount = 3,
        DeleteAccount = 4,
        AcceptNewCurrency = 5,
        ModifyServiceCharge = 6,
        TransactionHistory = 7,
        RevertTransaction = 8,
        LoginAsUser = 9,
        Exit = 10

    }

    public enum CustomerOption {
        ViewBalance = 1,
        DepositMoney = 2,
        WithdrawMoney = 3,
        TransferMoney = 4,
        TransactionHistory = 5,
        Exit = 6
    }

    class Driver {
        private List<Bank> Banks = new List<Bank>();
        private User Admin = new User("virendra", "pandey", "admin", true, true);
        #region[InputPassword]
        public void InputPassword(out string password) {
            password = string.Empty;
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter) {
                    break;
                }
            } while (true);
        }
        #endregion

        #region[EmployeeOption]
        public void EmployeeOptions() {
            Console.WriteLine("\nCHOOSE OPTION...");
            Console.WriteLine("Press 1 : To View All Accounts ");
            Console.WriteLine("Press 2 : To Create Account ");
            Console.WriteLine("Press 3 : To Update Customer Account ");
            Console.WriteLine("Press 4 : To Delete Account ");
            Console.WriteLine("Press 5 : To Accept New Currency ");
            Console.WriteLine("Press 6 : To Modify Service Charges ");
            Console.WriteLine("Press 7 : To View Transaction History ");
            Console.WriteLine("Press 8 : To Revert Transaction ");
            Console.WriteLine("Press 9 : To Log In As a Customer ");
            Console.WriteLine("Press 10 : To Exit ");
        }
        #endregion

        public void UpdateOption() {
            Console.WriteLine("\nCHOOSE OPTION...");
            Console.WriteLine("Press 1 : Update Fist Name ");
            Console.WriteLine("Press 2 : Update Last Name ");
            Console.WriteLine("Press 3 : Update Username ");
            Console.WriteLine("Press 4 : Update Password ");

        }

        public int InputIntRange(int a, int b) {
            int c = Int32.Parse(Console.ReadLine());
            if (c >= a && c <= b)
                return c;
            Console.WriteLine("Invalid Option, Try Again...");
            return InputIntRange(a, b);
        }

        public void ServiceChargeUpdateOption() {
            Console.WriteLine("\nCHOOSE OPTION...");
            Console.WriteLine("Press 1 : Same Bank RTGS");
            Console.WriteLine("Press 2 : Same Bank IMPS");
            Console.WriteLine("Press 3 : Other Bank RTGS");
            Console.WriteLine("Press 4 : Other Bank IMPS");
        }

        #region[EmployeeAction]
        public void EmployeeAction(Bank bank) {

            string username, password;
            Console.WriteLine("LOGIN");
            Console.WriteLine("Enter Username");
            username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            InputPassword(out password);
            User user = bank.Users.Where(u => u.Username.Equals(username) && u.IsEmployee && u.IsValid(password)).FirstOrDefault();
            if (user == null) {
                Console.WriteLine("Invalid User...");
                return;
            }

            while (true) {
                EmployeeOptions();
                int option = InputIntRange(1, 10);
                if (option == (int)EmployeeOption.ViewAllAccounts) {
                    Console.WriteLine("Accounts Ids are : ");
                    int i = 1;
                    foreach (Account a in bank.Accounts) {
                        Console.WriteLine(i++ + " " + a.Id);
                       
                    }
                }
                else if (option == (int)EmployeeOption.CreateAccounts) {
                    string firstname, lastname;
                    Console.WriteLine("Enter First Name : ");
                    firstname = Console.ReadLine();
                    Console.WriteLine("Enter Last Name : ");
                    lastname = Console.ReadLine();
                    Account account = new Account(firstname.Substring(0, 3) + DateTime.Now.ToString("dd/MM/yyyy"));
                    bank.Accounts.Add(account);
                    account.CreateAccount(bank, firstname, lastname);
                    Console.WriteLine("Account Opened Successfully...");

                }
                else if (option == (int)EmployeeOption.UpdateAccount) {
                    string accountId;
                    Console.WriteLine("Enter Account Id : ");
                    accountId = Console.ReadLine();
                    UpdateOption();
                    int opt = InputIntRange(1, 4);
                    string toUpdate;
                    Console.WriteLine("Enter " + (Update)opt);
                    toUpdate = Console.ReadLine();
                    Account account = bank.Accounts.Where(a => a.Id.Equals(accountId)).FirstOrDefault();
                    account.UpdateAccount(bank, toUpdate, opt);
                    Console.WriteLine("Updated Successfully...");

                }
                else if (option == (int)EmployeeOption.DeleteAccount) {
                    string accountId;
                    Console.WriteLine("Enter Account Id");
                    accountId = Console.ReadLine();
                    Account account = bank.Accounts.Where(a => a.Id.Equals(accountId)).FirstOrDefault();
                    if(account != null) {
                        account.DeleteAccount(bank);
                        bank.Accounts.Remove(account);
                        Console.WriteLine("Account Deactivated Successfully...");
                    }
                    else {
                        Console.WriteLine("Account Does not Exist");

                    }
                    
                }
                else if (option == (int)EmployeeOption.AcceptNewCurrency) {
                    string accountId;
                    decimal amt;
                    decimal exgRate;
                    Console.WriteLine("Enter Account Id : ");
                    accountId = Console.ReadLine();
                    Console.WriteLine("Enter Amount : ");
                    amt = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Exchange Rate : ");
                    exgRate = decimal.Parse(Console.ReadLine());
                    amt *= exgRate;

                    Account account = bank.Accounts.Where(a => a.Id.Equals(accountId)).FirstOrDefault();
                    account.AcceptNewCurrency(amt);
                
                    Console.WriteLine("Updated Balance : " + account.Balance);

                }
                else if (option == (int)EmployeeOption.ModifyServiceCharge) {
                    ServiceChargeUpdateOption();
                    int opt = InputIntRange(1, 4);
                    decimal forUpdate;
                    Console.WriteLine("Enter New " + (UpdateServiceChargeOf)opt);
                    forUpdate = decimal.Parse(Console.ReadLine());
                    bank.ModifyServiceCharges(opt, forUpdate);
                    Console.WriteLine("Updated Successfully...");
                }
                else if (option == (int)EmployeeOption.TransactionHistory) {
                    string accountId;
                    Console.WriteLine("Enter Account Id : ");
                    accountId = Console.ReadLine();
                    Account account = bank.Accounts.Where(a => a.Id.Equals(accountId)).FirstOrDefault();

                    foreach (Transaction txn in account.Transactions) {
                        Console.WriteLine("Id : " + txn.Id);
                        Console.WriteLine("Amount : " + txn.Amount + "   Type : " + txn.Type);
                    }

                }
                else if (option == (int)EmployeeOption.RevertTransaction) {
                    string transactionId, senderId;
                    Console.WriteLine("Enter Your Account Id ");
                    senderId = Console.ReadLine();
                    Console.WriteLine("Enter Transaction Id ");
                    transactionId = Console.ReadLine();
                    Account sender = bank.Accounts.Where(a => a.Id.Equals(senderId)).FirstOrDefault();
                    Transaction transaction = sender.Transactions.Where(txn => txn.Id.Equals(transactionId)).FirstOrDefault();
                    Account receiver = sender.GetAccountById(bank, transaction.ReceiverId);

                    string revertedTransactionId = transaction.RevertTransaction(bank, sender, receiver);
                    if (revertedTransactionId != null) {
                        Console.WriteLine("Invalid Id...");
                        continue;
                    }
                    Console.WriteLine("Transaction is Reverted Successfully...");
                    Console.WriteLine("Transaction Id = " + revertedTransactionId);


                }
                else if (option == (int)EmployeeOption.LoginAsUser) {
                    CustomerAction(bank);
                }
                else {
                    return;
                }
            }


        }
        #endregion

        #region[CustomerOption]
        public void CustomerOptions() {
            Console.WriteLine("\nCHOOSE OPTION...");
            Console.WriteLine("Press 1 : To View Balance ");
            Console.WriteLine("Press 2 : To Deposit Money ");
            Console.WriteLine("Press 3 : To Withdraw Money ");
            Console.WriteLine("Press 4 : To Transfer Money ");
            Console.WriteLine("Press 5 : To View Transaction History ");
            Console.WriteLine("Press 6 : To Exit ");

        }
        #endregion

        #region[CustomerAction]
        public void CustomerAction(Bank bank) {

            string username, password;
            Console.WriteLine("LOGIN");
            Console.WriteLine("Enter Username");
            username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            InputPassword(out password);

            User user = bank.Users.Where(u => u.Username.Equals(username) && u.IsValid(password)).FirstOrDefault();
            if (user == null) {
                return;
            }

            Account account = bank.Accounts.Where(a => a.Id.Equals(user.AccountId)).FirstOrDefault();
            while (true) {
                CustomerOptions();
                int option = InputIntRange(1, 6);
                if (option == (int)CustomerOption.ViewBalance) {
                    Console.WriteLine("Balance : " + account.Balance);
                }
                else if (option == (int)CustomerOption.DepositMoney) {
                    decimal amt;
                    Console.WriteLine("Enter Amount To Deposit : ");
                    amt = decimal.Parse(Console.ReadLine());
                    account.DepositMoney(amt);
                    Console.WriteLine("Money Deposited Successfully...");
                }
                else if (option == (int)CustomerOption.WithdrawMoney) {
                    decimal amt;
                    Console.WriteLine("Enter Amount To Deposit : ");
                    amt = decimal.Parse(Console.ReadLine());
                    decimal Amount = account.WithdrawMoney(amt);
                    if (Amount == 0) {
                        Console.WriteLine("Insufficient Balance...");
                    }
                    else {
                        Console.WriteLine("Withdrawal Successfully...");
                    }
                }
                else if (option == (int)CustomerOption.TransferMoney) {
                    string receiverId;
                    decimal amt;
                    string generatedTransactionId;
                    Console.WriteLine("Enter Receiver Account Id ");
                    receiverId = Console.ReadLine();

                    Account receiver = bank.Accounts.Where(a => a.Id.Equals(receiverId)).FirstOrDefault();
                    Console.WriteLine("Enter amount to Transfer : ");
                    amt = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Press 1 : Transfer Money through RTGS ");
                    Console.WriteLine("Press 2 : Transfer Money through IMPS ");
                    Transaction newTransaction = new Transaction();
                    account.Transactions.Add(newTransaction);
                    
                    int opt = InputIntRange(1, 2);
                    try {
                        if (opt == 1) {

                            generatedTransactionId = newTransaction.TransferMoneyThroughRTGS(bank, account, receiver, amt);
                        }
                        else if (opt == 2) {
                            generatedTransactionId = newTransaction.TransferMoneyThroughIMPS(bank, account, receiver, amt);
                        }
                        else {
                            Console.WriteLine("Invalid Option...");
                            continue;
                        }
                        Console.WriteLine("Money Transafer Successfully...");
                        Console.WriteLine("Transaction Id = " + generatedTransactionId);


                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }


                else if (option == (int)CustomerOption.TransactionHistory) {

                    foreach (Transaction txn in account.Transactions) {
                        Console.WriteLine("Id : " + txn.Id);
                        Console.WriteLine("Amount : " + txn.Amount + "   Type : " + txn.Type);
                    }
                }
                else {
                    return;
                }
            }


        }
        #endregion

        public void ActionType(Bank bank) {
            Console.WriteLine("Press 1 : Login As a Employee\nPress 2 : Login As a Customer");
            Console.WriteLine("Press 3 : Exit");
            int option;
            try {
                option = Int32.Parse(Console.ReadLine());
                if (option == (int)LoginAs.Employee) {

                    EmployeeAction(bank);
                }
                else if (option == (int)LoginAs.Customer) {

                    CustomerAction(bank);

                }
                else {
                    Console.WriteLine("Exit...");
                    return;
                }
            }
            catch (Exception) {
                Console.WriteLine("Invalid Option...");
                ActionType(bank);
            }
        }

        public void ShowBanks() {
            for (int i = 0; i < Banks.Count; i++) {
                Console.WriteLine("Press " + i + " To Enter :: " + Banks[i].Name);
            }
        }

        public void StartBankingSystem() {
            ShowBanks();
            int option = Int32.Parse(Console.ReadLine());
            try {
                Console.WriteLine("WELCOME TO " + Banks[option]);

            }
            catch (Exception) {
                Console.WriteLine("Invalid Option");
                SetUpBank();
            }
         
            ActionType(Banks[option]);

        }

        public Bank SetUpBank() {
            string bankname;
            Console.WriteLine("\nEnter Bank Name...");
            bankname = Console.ReadLine();
            if (bankname.Length > 2) {
                Banks.Add(new Bank(bankname));
                return Banks.Last();
            }
         
            Console.WriteLine( "Enter Valid Bank Name : atleast 3 letter word ");
            return null;
        }

        public void SetUpEmployee(Bank bank) {
            string firstname, lastname;
            Console.WriteLine("Enter Employee Firstname");
            firstname = Console.ReadLine();
            Console.WriteLine("Enter Employee Lastname");
            lastname = Console.ReadLine();
            User user = new User(firstname, lastname, bank.Name, true);
            bank.Users.Add(user);
  
        }

        public static void Main() {
            Driver driver = new Driver();

            string username, password;
            while (true) {
                
                Console.WriteLine("Admin Panel");
                Console.WriteLine("Want to Set Up a Bank? Press y/n");
                if ("n".Equals(Console.ReadLine())) {
                    
                    break;
                }
                Console.WriteLine("Enter Username ");
                username = Console.ReadLine();
                Console.WriteLine("Enter Password ");
                driver.InputPassword(out password);

                if (driver.Admin.Username.Equals(username) && driver.Admin.IsValid(password)) {
                    Bank bank = driver.SetUpBank();
                    while (bank != null) {
                        Console.WriteLine("Want to Create Employees? Press y/n");
                        if ("n".Equals(Console.ReadLine())) {
                           
                            break;
                        }
                        driver.SetUpEmployee(bank);
                    }
                    
                }
                 
                else
                    Console.WriteLine("Unauthorized Access...");
            }
            Console.Clear();
            while (true) {
                driver.StartBankingSystem();
                Console.WriteLine("Press y : Continue Banking...");
                Console.WriteLine("Press n : Exit Completely...");
                if ("n" == Console.ReadLine()) {
                    System.Environment.Exit(0);
                }
            }



        }
    }
}
