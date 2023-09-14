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
        public async Task MakeDeposit(HttpClient _http, JsonSerializerOptions joptions, IEnumerable<Account> accounts) 
        {
            var amount = ReadAndWrite("Amount to deposit: ");
            var accountId = ReadAndWrite("Account to deposit to: ");
        }
    }
}
