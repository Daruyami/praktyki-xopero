using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace CurrencyRatesFromAPI
{
    internal static class Program
    {
        public static List<ResponseData> ResponseData = new List<ResponseData>();
        public static Rate FromCurrency = new Rate(){ Code = "PLN", Mid = 1};
        public static List<Rate> ToCurrencies;

        public static void GetResponseData()
        {
            var response = new WebClient().DownloadString("http://api.nbp.pl/api/exchangerates/tables/a/?format=json");
            JsonConvert.PopulateObject(response, ResponseData);
        }

        public static void ShowDate()
        {
            Console.Out.WriteLine("Data from: "+ResponseData[0].EffectiveDate);
        }
        
        public static void ListAvailableCurrenciesCodes()
        {
            Console.Out.WriteLine("Available currencies codes: ");
            foreach (var rate in ResponseData[0].Rates)
            {
                Console.Out.WriteLine("Code: "+rate.Code);
            }
        }
        
        public static void ListRates()
        {
            Console.Out.WriteLine("Available currencies rates: ");
            foreach (var rate in ResponseData[0].Rates)
            {
                Console.Out.WriteLine("Code: "+rate.Code);
                Console.Out.WriteLine("Mid: "+rate.Mid);
                Console.Out.WriteLine("— — — — — — ");
            }
        }
        
        public static string GetUserInput(string message=null)
        {
            if (message != null)
                Console.Out.WriteLine(message);
            string input;
            do
                input = Console.ReadLine();
            while (string.IsNullOrEmpty(input));
            return input;
        }
        
        public static void DisplayHelp()
        {
            Console.Out.WriteLine("Available commands: ");
            Console.Out.WriteLine("?\tdisplays this message");
            Console.Out.WriteLine("list all\tlists available rates");
            Console.Out.WriteLine("list codes\tlists available currency codes");
            Console.Out.WriteLine("select from\tselects the currency to exchange from");
            Console.Out.WriteLine("select to\tselects the currency/ies to exchange to");
            Console.Out.WriteLine("exchange\texchanges from selected currency to selected currencies");
            Console.Out.WriteLine("date\tshows when was the data generated");
            Console.Out.WriteLine("exit\texits from the program");
        }

        public static bool InputCommands()
        {
            string input = GetUserInput("\nWaiting for user commands, to list available commands type: ?");
            switch (input)
            {
                case "?":
                    DisplayHelp();
                    break;
                case "list all":
                    ListRates();
                    break;
                case "list codes":
                    ListAvailableCurrenciesCodes();
                    break;
                case "date":
                    ShowDate();
                    break;
                case "select from":
                    SelectFrom();
                    break;
                case "select to":
                    SelectTo();
                    break;
                case "exchange":
                    Exchange();
                    break;
                case "exit":
                    return false;
                default:
                    DisplayHelp();
                    break;

            }
            return true;
        }

        public static void SelectFrom()
        {
            string input = "";
            while (input.Length != 3)
            {
                if (input == "q")
                    return;
                input = GetUserInput("Input the code of currency you want to exchange from\n[q] to cancel");
            }

            if (input == "PLN")
            {
                FromCurrency = new Rate() { Code = "PLN", Mid = 1 };
                return;
            }
            foreach (var rate in ResponseData[0].Rates)
                if (rate.Code == input)
                {
                    FromCurrency = rate;
                    return;
                }

            Console.Out.WriteLine("Wrong currency code! Use list codes or list all to see available codes!");
        }

        public static void SelectTo()
        {
            List<Rate> toCurrencies = new List<Rate>();
            while (true)
            {
                var input = GetUserInput("Input the code of currency you want to exchange to\n[q] to cancel, [s] to save");
                if (input == "q")
                    return;
                if (input == "s")
                    break;
                if (input.Length != 3)
                    Console.Out.WriteLine("All codes must be 3 characters long (ISO 4217:2015), input only one per prompt");
                else if (input == "PLN")
                    toCurrencies.Add(new Rate(){ Code = "PLN", Mid = 1 });
                else
                    foreach (var rate in ResponseData[0].Rates)
                    {
                        if (rate.Code == input)
                        {
                            toCurrencies.Add(rate);
                            break;
                        }
                    }
            }

            ToCurrencies = toCurrencies;
        }

        public static void Exchange()
        {
            decimal input = decimal.Parse(GetUserInput("Input a value"));
            Console.Out.WriteLine("From:\n\t"+FromCurrency.Code+":\t"+input);
            var totalValue = input * FromCurrency.Mid;
            Console.Out.WriteLine("To: ");
            foreach (var rate in ToCurrencies)
            {
                Console.Out.WriteLine("\t"+rate.Code+":\t"+(totalValue/rate.Mid).ToString("F"));
            }
        }

        public static void Main(string[] args)
        {
            GetResponseData();
            while(InputCommands());
        }
    }
}