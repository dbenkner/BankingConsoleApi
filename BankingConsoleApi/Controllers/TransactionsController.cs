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
            bool successAmount = decimal.TryParse(amountStr, out amount);
            bool successAccountId = int.TryParse(accountIdStr, out accountId);
            if (successAmount == false || successAccountId == false) 
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            bool accountExists = false;
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
            var newTransaction = new Transaction()
            {
                Id = 0,
                AccountId = accountId,
                PreviousBalance = (decimal)accounts.Where(x => x.Id == accountId).SingleOrDefault().Balance,
                TransactionType = "D"
            };
            await MakeTransaction(_http, joptions, newTransaction, amount);
            Console.WriteLine($"Deposited {amount} in account {accountId}");
            Console.WriteLine($"New Balance is {newTransaction.PreviousBalance + amount:c}");

        }

        public async Task MakeWithdraw(IEnumerable<Account> accounts)
        {
            decimal amount;
            int accountId;
            var amountStr = ReadAndWrite("Amount to withdraw: ");
            var accountIdStr = ReadAndWrite("Account to withdraw from: ");
            bool successAmount = decimal.TryParse(amountStr, out amount);
            bool successAccountId = int.TryParse(accountIdStr, out accountId);
            if (successAmount == false || successAccountId == false)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            bool accountExists = false;
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
            var newTransaction = new Transaction()
            {
                Id = 0,
                AccountId = accountId,
                PreviousBalance = (decimal)accounts.Where(x => x.Id == accountId).SingleOrDefault().Balance,
                TransactionType = "W"
            };
            if (amount > newTransaction.PreviousBalance)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            await MakeTransaction(_http, joptions, newTransaction, amount);
            Console.WriteLine($"Withdrew {amount} from account {accountId}");
            Console.WriteLine($"New Balance is {newTransaction.PreviousBalance - amount:c}");

        }

        public async Task Transfer()
        {
            decimal amount;
            int fromAccountId;
            int toAccountId;
            var amountStr = ReadAndWrite("Amount to transfer ");
            var fromAccountIdStr = ReadAndWrite("Account to transfer from: ");
            var toAccountIdStr = ReadAndWrite("Account to transfer to: ");
            bool successAmount = decimal.TryParse(amountStr, out amount);
            bool successFromAccountId = int.TryParse(fromAccountIdStr, out fromAccountId);
            bool successToAccountId = int.TryParse(toAccountIdStr, out toAccountId);
        }

        private async Task MakeTransaction(HttpClient _http, JsonSerializerOptions joptions, Transaction newTrans, decimal amount)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"{BaseURL}/api/transactions/{amount}");
            var json = JsonSerializer.Serialize<Transaction>(newTrans, joptions);
            req.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _http.SendAsync(req);
        }
    }
}
