﻿using System;
namespace BankingConsoleApi.Controllers
{
	public class GeneralController
	{
        public string ReadAndWrite(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public string OptionsMenu()
        {
            Console.WriteLine("Please select an option.");
            Console.WriteLine("1. Get your Balance");
            Console.WriteLine("2. Make Deposit");
            Console.WriteLine("3. Make Withdrawal");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Get Transactions");
            Console.WriteLine("6. Exit");
            return ReadAndWrite("Please select a number: ");
        }
    }
}

