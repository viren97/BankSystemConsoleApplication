using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem {
    public class User {
        public string Id { get; set; }
        private string _password;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { set { _password = value; } }
        public bool IsEmployee { get; }
        public bool IsAdmin { get;  }
        public string AccountId { get; }

        public User(string firstname, string lastname, string bankname, bool isEmployee = false, bool isAdmin = false) {
            Id = firstname.Substring(0, 3) + DateTime.Now.ToString("dd/MM/yyyy");
            Firstname = firstname;
            Lastname = lastname;
            Username = firstname + "." + lastname + "@" + bankname + (isEmployee ? (isAdmin ? "" : "emp") : "cu");
            Password = bankname + (isEmployee ? (isAdmin ? "" : "emp") : "cu") + firstname + "#2020";
            IsEmployee = isEmployee;
            IsAdmin = isAdmin;
            AccountId = firstname.Substring(0, 3) + DateTime.Now.ToString("dd/MM/yyyy");
        }

        public bool IsValid(string password) {
            return password.Equals(_password);
        }
        



    }
}
