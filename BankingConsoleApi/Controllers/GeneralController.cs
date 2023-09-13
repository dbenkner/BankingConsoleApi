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
    }
}

