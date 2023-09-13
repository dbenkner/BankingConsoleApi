using BankingConsoleApi;
using BankingConsoleApi.Controllers;

bool programRunning = true;
string userInput = string.Empty;
CustomersController _cusCtrlr = new CustomersController();
Customer? customer = null;
GeneralController generalCtrlr = new GeneralController();

while (programRunning == true)
{
    while (customer == null)
    {
        customer = await _cusCtrlr.LoginCustomer();
        if (customer == null)
        {
            Console.WriteLine("Login failed. Please try again.");
        }
    }
    Console.WriteLine("Log in Sucuess!");
    generalCtrlr.OptionsMenu();
    programRunning = false;
}
