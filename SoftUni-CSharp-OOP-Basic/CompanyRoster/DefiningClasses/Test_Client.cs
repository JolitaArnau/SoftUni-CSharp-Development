using System;
using System.Collections.Generic;

class Test_Client
{
    static void Main()
    {
        var commands = Console.ReadLine();

        var bankAccounts = new Dictionary<int, BankAccount>();

        while (!commands.Equals("End"))
        {
            var commandTokens = commands.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var command = commandTokens[0];

            switch (command)
            {
                case "Create":
                    Create(commandTokens, bankAccounts);
                    break;
                case "Deposit":
                    Deposit(commandTokens, bankAccounts);
                    break;
                case "Withdraw":
                    Withdraw(commandTokens, bankAccounts);
                    break;
                case "Print":
                    Print(commandTokens, bankAccounts);
                    break;
            }

            commands = Console.ReadLine();
        }
    }

    private static void Print(string[] commandTokens, Dictionary<int, BankAccount> bankAccounts)
    {
        int id = int.Parse(commandTokens[1]);
        if (bankAccounts.ContainsKey(id))
        {
            var wantedId = bankAccounts[id].Id;
            var balance = bankAccounts[id].Balance;
            Console.WriteLine($"Account ID{wantedId}, balance {balance:f2}");
        }
        else
        {
            Console.WriteLine("Account does not exist");
        }
    }

    private static void Withdraw(string[] commandTokens, Dictionary<int, BankAccount> bankAccounts)
    {
        int id = int.Parse(commandTokens[1]);
        decimal amount = decimal.Parse(commandTokens[2]);

        if (!bankAccounts.ContainsKey(id))
        {
            Console.WriteLine("Account does not exist");
        }

        if (bankAccounts.ContainsKey(id) && bankAccounts[id].Balance < amount)
        {
            Console.WriteLine("Insufficient balance");
        }

        if (bankAccounts.ContainsKey(id) && bankAccounts[id].Balance >= amount)
        {
            bankAccounts[id].Withdraw(amount);
        }
    }

    private static void Deposit(string[] commandTokens, Dictionary<int, BankAccount> bankAccounts)
    {
        int id = int.Parse(commandTokens[1]);
        decimal amount = decimal.Parse(commandTokens[2]);

        if (!bankAccounts.ContainsKey(id))
        {
            Console.WriteLine("Account does not exist");
        }
        else
        {
            bankAccounts[id].Deposit(amount);
        }
    }

    private static void Create(string[] commandTokens, Dictionary<int, BankAccount> bankAccounts)
    {
        int id = int.Parse(commandTokens[1]);

        if (bankAccounts.ContainsKey(id))
        {
            Console.WriteLine("Account already exists");
        }
        else
        {
            BankAccount account = new BankAccount();
            account.Id = id;
            bankAccounts.Add(id, account);
        }
    }
}

