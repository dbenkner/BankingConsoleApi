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

        public async Task Transfer(IEnumerable<Account> accounts)
        {
            decimal amount;
            int fromAccountId;
            int toAccountId;
            var amountStr = ReadAndWrite("Amount to transfer: ");
            var fromAccountIdStr = ReadAndWrite("Account to transfer from: ");
            var toAccountIdStr = ReadAndWrite("Account to transfer to: ");
            bool successAmount = decimal.TryParse(amountStr, out amount);
            bool successFromAccountId = int.TryParse(fromAccountIdStr, out fromAccountId);
            bool successToAccountId = int.TryParse(toAccountIdStr, out toAccountId);
            if (successAmount == false || successFromAccountId == false || successToAccountId == false)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            bool fromAccountExists = false;
            bool toAccountExists = false;
            foreach (var account in accounts)
            {
                if (fromAccountId == account.Id)
                {
                    fromAccountExists = true;
                }
                if (toAccountId == account.Id)
                {
                    toAccountExists = true;
                }
            }
            if (fromAccountExists == false || toAccountExists == false)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            var fromTransaction = new Transaction()
            {
                Id = 0,
                AccountId = fromAccountId,
                PreviousBalance = (decimal)accounts.Where(x => x.Id == fromAccountId).SingleOrDefault().Balance,
                TransactionType = "W"
            };
            var toTransaction = new Transaction()
            {
                Id = 0,
                AccountId = toAccountId,
                PreviousBalance = (decimal)accounts.Where(x => x.Id == toAccountId).SingleOrDefault().Balance,
                TransactionType = "D"
            };
            if (amount > fromTransaction.PreviousBalance)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            await MakeTransaction(_http, joptions, fromTransaction, amount);
            await MakeTransaction(_http, joptions, toTransaction, amount);
            Console.WriteLine("Transaction Completed!");
        }

        public async Task GetAllTrans(IEnumerable<Account> accounts)
        {
            int accountId;
            var accountIdStr = ReadAndWrite("Account number: ");
            bool successAccountId = int.TryParse(accountIdStr, out accountId);
            if (successAccountId == false)
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
            var transactions = await GetTransactions(_http, joptions, accountId);
            Console.WriteLine("Transaction ID | Type | Previous Balance | New Balance | Transaction Total | Transaction Date");
            foreach (var transaction in transactions)
            {
                decimal total;
                if ( transaction.TransactionType == "W")
                {
                    total = transaction.PreviousBalance - transaction.NewBalance;
                }
                else
                {
                    total = transaction.NewBalance - transaction.PreviousBalance;
                }
                Console.WriteLine($"{transaction.Id,10} | {transaction.TransactionType} | {transaction.PreviousBalance:c} | {transaction.NewBalance:c} | {total:c} | {transaction.CreatedDate:d}");
            }
        }

        private async Task<IEnumerable<Transaction>> GetTransactions(HttpClient _http, JsonSerializerOptions joptions, int accountId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{BaseURL}/api/Transactions/Account/{accountId}");
            HttpResponseMessage res = await _http.SendAsync(req);
            var json = await res.Content.ReadAsStringAsync();
            var transactions = (IEnumerable<Transaction>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<Transaction>), joptions);
            return transactions;
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
