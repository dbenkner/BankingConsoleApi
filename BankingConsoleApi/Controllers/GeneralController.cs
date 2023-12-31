﻿using System;
using System.Text.Json;

namespace BankingConsoleApi.Controllers
{
	public static class GeneralController
	{
        public static string BaseURL = "http://localhost:5555";
        public static HttpClient _http = new HttpClient();
        public static JsonSerializerOptions joptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static string ReadAndWrite(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public static string OptionsMenu()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("1. Get your Balance");
            Console.WriteLine("2. Make Deposit");
            Console.WriteLine("3. Make Withdrawal");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Get Transactions");
            Console.WriteLine("6. Open a New Account");
            Console.WriteLine("7. Exit");
            return ReadAndWrite("Please select a number: ");
        }
    }
}

