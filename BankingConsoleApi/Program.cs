using BankingConsoleApi;
using BankingConsoleApi.Controllers;


string userInput = string.Empty;
bool ProgramRunning = true;
Customer? customer = null;
IEnumerable<Account> accounts;

while (customer == null)
{
        customer = await CustomersController.LoginCustomer();
        if (customer == null)
        {
            Console.WriteLine("Login failed. Please try again.");
        }
}
Console.WriteLine("Log in Success!");
Console.WriteLine($"Welcome! {customer.Name}!");


while (ProgramRunning == true)
{
    var option = GeneralController.OptionsMenu();
    switch (option)
    {
        case "1":
            accounts = await AccountsController.GetBalance(customer.Id);
            break;
        case "2":
            accounts = await AccountsController.GetBalance(customer.Id);
            await TransactionsController.MakeDeposit(accounts);
            break;
        case "3":
            accounts = await AccountsController.GetBalance(customer.Id);
            await TransactionsController.MakeWithdraw(accounts);
            break;
        case "4":
            accounts = await AccountsController.GetBalance(customer.Id);
            await TransactionsController.Transfer(accounts);
            break;
        case "5":
            accounts = await AccountsController.GetBalance(customer.Id);
            await TransactionsController.GetAllTrans(accounts);
            break;
        case "6":
            await AccountsController.OpenAccount(customer.Id);
            break;
        case "7":
            ProgramRunning = false;
            break;
        default:
            Console.WriteLine("Please select a valid option.");
            break;
    }
}






