using BankingConsoleApi;
using BankingConsoleApi.Controllers;


string userInput = string.Empty;
bool ProgramRunning = true;
CustomersController _cusCtrlr = new CustomersController();
Customer? customer = null;
GeneralController generalCtrlr = new GeneralController();
AccountsController acctCtrlr = new AccountsController();
TransactionsController transCtrlr = new TransactionsController();
IEnumerable<Account> accounts;

while (customer == null)
{
        customer = await _cusCtrlr.LoginCustomer();
        if (customer == null)
        {
            Console.WriteLine("Login failed. Please try again.");
        }
}
Console.WriteLine("Log in Success!");
Console.WriteLine($"Welcome! {customer.Name}!");


while (ProgramRunning == true)
{
    var option = generalCtrlr.OptionsMenu();
    switch (option)
    {
        case "1":
            accounts = await acctCtrlr.GetBalance(customer.Id);
            break;
        case "2":
            accounts = await acctCtrlr.GetBalance(customer.Id);
            await transCtrlr.MakeDeposit(accounts);
            break;
        case "3":
            break;
        case "4":
            break;
        case "5":
            break;
        case "6":
            ProgramRunning = false;
            break;
        default:
            Console.WriteLine("Please select a valid option.");
            break;
    }
}






