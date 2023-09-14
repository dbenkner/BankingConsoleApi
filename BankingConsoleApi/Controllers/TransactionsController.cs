using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingConsoleApi.Controllers
{
    public static class TransactionsController
    {

        public static async Task MakeDeposit(IEnumerable<Account> accounts)
        {
            var amount = CheckAmount(GeneralController.ReadAndWrite("Amount to deposit: "));
            var accountId = CheckId(GeneralController.ReadAndWrite("Account to deposit to: "), accounts);

            if (amount == null || accountId == null)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
         
            Transaction? newTransaction = await CompleteTransaction(accountId, amount, "D", accounts);
            Console.WriteLine($"Deposited {amount:c} in account {accountId}");

        }

        public static async Task MakeWithdraw(IEnumerable<Account> accounts)
        {
            var amount = CheckAmount(GeneralController.ReadAndWrite("Amount to withdraw: "));
            var accountId = CheckId(GeneralController.ReadAndWrite("Account to withdraw from: "), accounts);
            if (amount == null || accountId == null)
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            Transaction? newTransaction = await CompleteTransaction(accountId, amount, "W", accounts);
            if (newTransaction == null)
            {
                Console.WriteLine("Cannot Overdraw Account!");
                return;
            }
            Console.WriteLine($"Withdrew {amount:c} from account {accountId}");
        }

        private static async Task<Transaction> CompleteTransaction(int? accountId, decimal? amount, string type, IEnumerable<Account> accounts)
        {
            var newTransaction = new Transaction()
            {
                Id = 0,
                AccountId = (int)accountId,
                PreviousBalance = (decimal)accounts.Where(x => x.Id == accountId).SingleOrDefault().Balance,
                TransactionType = type
            };
            if (type == "W")
            {
                if (amount > newTransaction.PreviousBalance)
                {
                    return null;
                }
            }
            await MakeTransaction(GeneralController._http, GeneralController.joptions, newTransaction, (decimal)amount);
            return newTransaction;
        }
        public static async Task Transfer(IEnumerable<Account> accounts)
        {
            var amount = CheckAmount(GeneralController.ReadAndWrite("Amount to transfer: "));
            var fromAccountId = CheckId(GeneralController.ReadAndWrite("Account to transfer from: "), accounts);
            var toAccountId = CheckId(GeneralController.ReadAndWrite("Account to transfer to: "), accounts);

            if (amount == null || fromAccountId == null || toAccountId == null)
            {
                Console.WriteLine("Invalid Input");
                return;
            }

            Transaction? fromTransaction = await CompleteTransaction(fromAccountId, amount, "W", accounts);
            if (fromTransaction == null)
            {
                Console.WriteLine("Cannot Overdraw Account!");
                return;
            }

            Transaction? toTransaction = await CompleteTransaction(toAccountId, amount, "D", accounts);

            Console.WriteLine("Transfer Completed!");
        }

        public static async Task GetAllTrans(IEnumerable<Account> accounts)
        {
            int accountId;
            var accountIdStr = GeneralController.ReadAndWrite("Account number: ");
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
            var transactions = await GetTransactions(GeneralController._http, GeneralController.joptions, accountId);
            Console.WriteLine("Transaction ID | Type | Previous Balance | New Balance | Transaction Total | Transaction Date");
            foreach (var transaction in transactions)
            {
                decimal total;
                if (transaction.TransactionType == "W")
                {
                    total = transaction.PreviousBalance - transaction.NewBalance;
                }
                else
                {
                    total = transaction.NewBalance - transaction.PreviousBalance;
                }
                Console.WriteLine($"{transaction.Id,14} | {transaction.TransactionType,4} | {transaction.PreviousBalance,16:c} | {transaction.NewBalance,11:c} | {total,17:c} | {transaction.CreatedDate,16:d}");
            }
        }

        private static async Task<IEnumerable<Transaction>> GetTransactions(HttpClient _http, JsonSerializerOptions joptions, int accountId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{GeneralController.BaseURL}/api/Transactions/Account/{accountId}");
            HttpResponseMessage res = await _http.SendAsync(req);
            var json = await res.Content.ReadAsStringAsync();
            var transactions = (IEnumerable<Transaction>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<Transaction>), joptions);
            return transactions;
        }

        private static async Task MakeTransaction(HttpClient _http, JsonSerializerOptions joptions, Transaction newTrans, decimal amount)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"{GeneralController.BaseURL}/api/transactions/{amount}");
            var json = JsonSerializer.Serialize<Transaction>(newTrans, joptions);
            req.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _http.SendAsync(req);
        }

        private static decimal? CheckAmount(string value)
        {
            decimal amount;
            bool successAmount = decimal.TryParse(value, out amount);
            if (successAmount == false)
            {
                return null;
            }
            return amount;
        }

        public static int? CheckId(string value, IEnumerable<Account> accounts)
        {
            int id;
            bool successId = int.TryParse(value, out id);
            if (successId == false)
            {
                return null;
            }

            bool accountExists = false;
            foreach (var account in accounts)
            {
                if (id == account.Id)
                {
                    accountExists = true;
                }
            }
            if (accountExists == false)
            {
                return null;
            }

            return id;
        }
    }
}