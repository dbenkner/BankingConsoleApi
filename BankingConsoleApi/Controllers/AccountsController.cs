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
			foreach (var account in accounts)
			{
				Console.WriteLine($"{account.Id,10} | {account.Description,12} | {account.Type,12} | {account.Balance,15:c}");
			}
			return accounts;
		}

		public static async Task OpenAccount(int customerId)
		{
			Console.WriteLine("What kind of account?");
			Console.WriteLine("1. Checking");
			Console.WriteLine("2. Savings");
			var choice = GeneralController.ReadAndWrite("Please select a number: ");

			switch (choice)
			{
				case "1":
					await AddAccount(GeneralController._http, GeneralController.joptions, customerId, "CK");
					Console.WriteLine("Account Opened!");
					break;
				case "2":
                    await AddAccount(GeneralController._http, GeneralController.joptions, customerId, "SV");
                    Console.WriteLine("Account Opened!");
                    break;
				default:
					Console.WriteLine("Invalid Input");
					return;
			}
		}

		private static async Task<IEnumerable<Account>> GetAccounts(HttpClient _http, JsonSerializerOptions joptions, int customerId)
		{
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{GeneralController.BaseURL}/api/Accounts/Customer/{customerId}");
			HttpResponseMessage res = await _http.SendAsync(req);
			var json = await res.Content.ReadAsStringAsync();
			var accounts = (IEnumerable<Account>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<Account>), joptions);
			return accounts;
		}

		private static async Task AddAccount(HttpClient _http, JsonSerializerOptions joptions, int customerId, string type)
		{
			var name = GeneralController.ReadAndWrite("Name for Account: ");

			var account = new Account()
			{
				Id = 0,
				Type = type,
				Description = name,
				Balance = 0.0m,
				CustomerId = customerId
			};

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, $"{GeneralController.BaseURL}/api/Customers/{customerId}/Add{type}");
            var json = JsonSerializer.Serialize<Account>(account, joptions);
            req.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _http.SendAsync(req);
            return;
        }
	}
}


