using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsoleApi
{
    public class Account
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal InterestRate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

    }
}
