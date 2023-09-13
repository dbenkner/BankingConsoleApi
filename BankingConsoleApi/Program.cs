using BankingConsoleApi.Controllers;

bool programRunning = true;
string userInput = string.Empty;
CustomersController _cusCtrlr = new CustomersController();
while (programRunning == true)
{
    var customer = _cusCtrlr.LoginCustomer();
}
