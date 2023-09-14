using System;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{
	public class AccountsController : GeneralController
	{
		public async Task<IEnumerable<Account>> GetBalance(int customerId)
		{
			var accounts = await GetAccounts(_http, joptions, customerId);
			Console.WriteLine("Account ID | Account Desc | Account Type | Account Balance");
			foreach(var account in accounts)
			{
				Console.WriteLine($"{account.Id,10} | {account.Description,12} | {account.Type,12} | {account.Balance,15:c}");
			}
			return accounts;
		}

        private async Task<IEnumerable<Account>> GetAccounts(HttpClient _http, JsonSerializerOptions joptions, int customerId)
		{
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{BaseURL}/api/Accounts/Customer/{customerId}");
			HttpResponseMessage res = await _http.SendAsync(req);
			var json = await res.Content.ReadAsStringAsync();
			var accounts = (IEnumerable<Account>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<Account>), joptions);
			return accounts;
		}
	}
}

