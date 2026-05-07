using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Session04_EFcore
{
    internal class Account
    {
        [Key]
        public int AccountNumber { get; set; }

        public decimal CurrentBalance { get; set; }

        public string AccountType { get; set; }

        public DateTime OpeningDate { get; set; }

        // Foreign Key
        public int BranchCode { get; set; }

        public Branch Branch { get; set; }

        // Navigation Property
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
