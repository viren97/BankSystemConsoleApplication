using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem {
    public class Bank {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        private List<User> _user = new List<User>();
        private List<Account> _accounts = new List<Account>();
        private ServiceCharges _serviceCharges = new ServiceCharges();

        public Bank(string bankname) {
            Id = bankname.Substring(0, 3) + DateTime.Now.ToString();
            Name = bankname;
            Currency = "INR";
        }

        public ServiceCharges ServiceCharges {
            get {
                return _serviceCharges;
            }
        }

        public List<Account> Accounts {
            get {
                return _accounts;
            }
            set {
                _accounts = value;
            }
        }

        public List<User> Users {
            get {
                return _user;
            }
            set {
                _user = value;
            }

        }

        public void ModifyServiceCharges(int option, decimal data) {
            if (option == (int)UpdateServiceChargeOf.sameBankRTGS) {
                ServiceCharges.SameBankRTGS = data;
            }
            else if (option == (int)UpdateServiceChargeOf.sameBankIMPS) {
                ServiceCharges.SameBankIMPS = data;
            }
            else if (option == (int)UpdateServiceChargeOf.otherBankRTGS) {
                ServiceCharges.OtherBankRTGS = data;
            }
            else if (option == (int)UpdateServiceChargeOf.otherBankIMPS) {
                ServiceCharges.OtherBankIMPS = data;
            }
            else {
                return;
            }
        }

    }

}

