using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsoleApi.Controllers
{

    public class CustomersController : GeneralController
    {
        public Customer LoginCustomer()
        {
            var CardCode = ReadAndWrite("Please enter your Card Code: ");
            var PinCode = ReadAndWrite("Please enter you Pin Code: ");
        }

        
    }
}
