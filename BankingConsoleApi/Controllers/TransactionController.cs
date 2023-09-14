using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingConsoleApi.Controllers
{
    public class TransactionController : GeneralController
    {
        public async Task MakeDeposit(HttpClient _http, JsonSerializerOptions joptions, int AccountId) 
        {
            
        }
    }
}
