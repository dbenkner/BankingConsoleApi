using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingConsoleApi.Controllers
{
    public class TransactionsController : GeneralController
    {
        public async Task MakeDeposit(IEnumerable<Account> accounts) 
        {
            decimal amount;
            int accountId;
            var amountStr = ReadAndWrite("Amount to deposit: ");
            var accountIdStr = ReadAndWrite("Account to deposit to: ");
            bool accountExists = false;
            bool successAmount = decimal.TryParse(amountStr, out amount);
            bool successAccountId = int.TryParse(accountIdStr, out accountId);
            if (successAmount == false || successAccountId == false) 
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            foreach (var account in accounts)
            {
                if (accountId == account.Id)
                {
                    accountExists = true;
                }
            }
            if (accountExists == false)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
        }
    }
}
