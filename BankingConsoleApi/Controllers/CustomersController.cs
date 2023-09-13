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

        public Customer LoginCustomer()
        {
            var CardCode = ReadAndWrite("Please enter your Card Code: ");
            var PinCode = ReadAndWrite("Please enter you Pin Code: ");

        }
        async Task<Customer> LogIn(HttpClient _http, JsonSerializerOptions joptions, int Cardcode, int Pincode)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{BaseURL}/api/customers/{Cardcode}/{Pincode}");
        }
    }
}
