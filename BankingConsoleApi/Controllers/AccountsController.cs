using System;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{
	public static class AccountsController
	{
		public static async Task<IEnumerable<Account>> GetBalance(int customerId)
		{
			var accounts = await GetAccounts(GeneralController._http, GeneralController.joptions, customerId);
			Console.WriteLine("Account ID | Account Desc | Account Type | Account Balance");
			foreach(var account in accounts)
			{
				Console.WriteLine($"{account.Id,10} | {account.Description,12} | {account.Type,12} | {account.Balance,15:c}");
			}
			return accounts;
		}

        private static async Task<IEnumerable<Account>> GetAccounts(HttpClient _http, JsonSerializerOptions joptions, int customerId)
		{
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{GeneralController.BaseURL}/api/Accounts/Customer/{customerId}");
			HttpResponseMessage res = await _http.SendAsync(req);
			var json = await res.Content.ReadAsStringAsync();
			var accounts = (IEnumerable<Account>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<Account>), joptions);
			return accounts;
		}
	}
}

