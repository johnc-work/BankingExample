using BankExample.Service;
using BankingExample.Models;
using System;

namespace BankingExample
{
    class Program
    {

        //TODO logging, sql dal, 
        static void Main(string[] args)
        {
            var bankName = "Default";
            var csvDal = new BankExample.DAL.CsvDataService(bankName);

            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, csvDal, bankConfig);
            string[] commands = new string[1] { "?" };
            while (commands.Length > 0 && commands[0].ToUpper()[0] != 'Q')
            {
                switch (commands[0].ToUpper()[0])
                {
                    case 'A':
                        HandleAccountInfo(commands, bank);
                        break;
                    case 'D':
                        HandleDeposit(commands, bank);
                        break;
                    case 'W':
                        HandleWithdraw(commands, bank);
                        break;
                    case 'T':
                        HandleTransfer(commands, bank);
                        break;
                    case 'S':
                        bankName = commands[1];
                        csvDal = new BankExample.DAL.CsvDataService(bankName);
                        bank = new BankService(bankName, csvDal, bankConfig);//would normally need to load a new config at this point
                        break;
                    default:
                        ShowCommands();
                        break;
                }

                commands = Console.ReadLine().Split(" ");
            }

            Console.WriteLine("Have a nice day!");

        }

        private static void HandleAccountInfo(string[] args, BankService service)
        {
            if (args.Length == 2 && !string.IsNullOrWhiteSpace(args[1]))
            {
                try
                {
                    var summary = service.GetAccountSummary(args[1]);
                    Console.WriteLine(summary);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("Deposit Commands: D [Account Number] [Amount]\r\n Example: D 12345 50.00");
            }

        }
        private static void HandleDeposit(string[] args, BankService service)
        {
            double amount;
            if (args.Length == 3 && !string.IsNullOrWhiteSpace(args[1]) && double.TryParse(args[2], out amount))
            {
                try
                {
                    service.AccountDeposit(args[1], amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("Deposit Commands: D [Account Number] [Amount]\r\n Example: D 12345 50.00");
            }

        }
        private static void HandleWithdraw(string[] args, BankService service)
        {
            double amount;
            if (args.Length == 3 && !string.IsNullOrWhiteSpace(args[1]) && double.TryParse(args[2], out amount))
            {
                try
                {
                    service.AccountWithdraw(args[1], amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("Withdraw Commands: W [Account Number] [Amount]\r\n Example: W 12345 40.00");
            }

        }
        private static void HandleTransfer(string[] args, BankService service)
        {
            double amount;
            if (args.Length == 4 && !string.IsNullOrWhiteSpace(args[3]) && double.TryParse(args[3], out amount))
            {
                try
                {
                    service.AccountTransfer(args[1], args[2], amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR\r\n{ex.Message}\r\n\r\n{ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("Withdraw Commands: T [Sending Account Number] [Recieving Account Number] [Amount]\r\n Example: T 12345 54321 15.00");
            }

        }

        private static void ShowCommands()
        {
            var message = @"A - Account Info
D - Deposit 
W - Withdraw
T - Transfer
S - Switch Banks
";
            Console.WriteLine(message);
        }
    }
}
