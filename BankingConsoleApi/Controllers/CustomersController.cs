using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{

    public static class CustomersController
    {
        public static async Task<Customer?> LoginCustomer()
        {
            var CardCodeStr = GeneralController.ReadAndWrite("Please enter your Card Code: ");
            var PinCodeStr = GeneralController.ReadAndWrite("Please enter you Pin Code: ");
            int CardCodeInt;
            int PinCodeInt;
            bool successCardCode = int.TryParse(CardCodeStr, out CardCodeInt);
            bool successPinCode = int.TryParse(PinCodeStr, out PinCodeInt);
            if (successCardCode == false || successPinCode == false)
            {
                return null;
            }
            var customer = await LogIn(GeneralController._http, GeneralController.joptions, CardCodeInt, PinCodeInt);
            if (customer.Id == 0)
            {
                return null;
            }

            return customer;
        }
        private static async Task<Customer> LogIn(HttpClient _http, JsonSerializerOptions joptions, int Cardcode, int Pincode)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{GeneralController.BaseURL}/api/customers/{Cardcode}/{Pincode}");
            HttpResponseMessage response = await _http.SendAsync(req);
            var json = await response.Content.ReadAsStringAsync();
            var customer = (Customer?)JsonSerializer.Deserialize(json, typeof(Customer), joptions);
            return customer;
        }
    }
}
