using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Session04_EFcore
{
    internal class Transaction
    {
        [Key]
        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string Note { get; set; }

        // Foreign Key
        public int AccountNumber { get; set; }

        public Account Account { get; set; }
    }
}
