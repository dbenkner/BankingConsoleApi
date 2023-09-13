using System;
namespace BankingConsoleApi.Controllers
{
	public class GeneralController
	{
        public string ReadAndWrite(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public void OptionsMenu()
        {
            Console.WriteLine("Please select an option.");
            Console.WriteLine("1. Get your Balance");
            Console.WriteLine("2. Make Deposit");
            Console.WriteLine("3. Make Withdrawal");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Get transactions");
            Console.WriteLine("6. Exit");
            string input = ReadAndWrite("Please select a number: ");
            switch (input)
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
                    Console.WriteLine("Please select a valid option");
                    OptionsMenu();
                    break;
            }
        }
    }
}

