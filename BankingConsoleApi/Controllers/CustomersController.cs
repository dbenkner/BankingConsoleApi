using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{

    public class CustomersController : GeneralController
    {
        const string BaseURL = "http://localhost:5555";
        HttpClient _http = new HttpClient();
        JsonSerializerOptions joptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public async Task<Customer> LoginCustomer()
        {
            var CardCodeStr = ReadAndWrite("Please enter your Card Code: ");
            var PinCodeStr = ReadAndWrite("Please enter you Pin Code: ");
            int CardCodeInt;
            int PinCodeInt;
            bool successCardCode = int.TryParse(CardCodeStr, out CardCodeInt);
            bool successPinCode = int.TryParse(PinCodeStr, out PinCodeInt);
            if (successCardCode == false || successPinCode == false)
            {
                Console.WriteLine("Please try again.");
                return null;
            }
            var customer = await LogIn(_http, joptions, CardCodeInt, PinCodeInt);
            if (customer == null)
            {
                Console.WriteLine("Log on failed");
                return null;
            }
            
            return customer;
        }
        public async Task<Customer> LogIn(HttpClient _http, JsonSerializerOptions joptions, int Cardcode, int Pincode)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{BaseURL}/api/customers/{Cardcode}/{Pincode}");
            HttpResponseMessage response = await _http.SendAsync(req);
            var json = await response.Content.ReadAsStringAsync();
            var customer = (Customer?)JsonSerializer.Deserialize(json, typeof(Customer), joptions);
            return customer;
        }
    }
}
