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
    Console.WriteLine("Log in Success!");

    var option = generalCtrlr.OptionsMenu();
    switch (option)
    {
        case "1":
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;
        case "5":
            break;
        case "6":
            break;
        default:
            Console.WriteLine("Please select a valid option.");
            OptionsMenu();
            break;
    }





    programRunning = false;
}
