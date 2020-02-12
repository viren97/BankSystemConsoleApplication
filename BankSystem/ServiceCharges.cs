using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem {
    public class ServiceCharges {
        public decimal SameBankRTGS { get; set; }
        public decimal SameBankIMPS { get; set; }
        public decimal OtherBankRTGS { get; set; }
        public decimal OtherBankIMPS { get; set; }

        public ServiceCharges() {
            SameBankRTGS = 0;
            SameBankIMPS = 5;
            OtherBankRTGS = 2;
            OtherBankIMPS = 6;
        }
    }
}
