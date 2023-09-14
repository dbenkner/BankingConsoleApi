using System;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{
	public class AccountsController : GeneralController
	{
		public async Task<IEnumerable<Account>> GetBalance(int customerId)
		{
			return await GetAccounts(_http, joptions, customerId);
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

