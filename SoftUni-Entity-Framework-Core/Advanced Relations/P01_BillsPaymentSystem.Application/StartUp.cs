namespace P01_BillsPaymentSystem.Application
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using System;
    using Models;


    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var dbContext = new BillsPaymentSystemContext())
            {
                dbContext.Database.EnsureDeleted();

                dbContext.Database.EnsureCreated();

                Seed(dbContext);

                Console.Write("Please enter a user Id: ");

                var userId = int.Parse(Console.ReadLine());

                var isUserFound = dbContext.Users.Find(userId) != null;

                if (isUserFound)
                {
                    var user = dbContext.Users
                        .Where(u => u.UserId == userId)
                        .Select(u => new
                        {
                            Name = $"{u.FirstName} {u.LastName}",
                            BankAccounts = u.PaymentMethods
                                .Where(pm => pm.Type == Models.Type.BankAccount)
                                .Select(pm => pm.BankAccount)
                                .ToList(),
                            CreditCards = u.PaymentMethods
                                .Where(pm => pm.Type == Models.Type.CreditCard)
                                .Select(pm => pm.CreditCard)
                                .ToList()
                        })
                        .First();

                    Console.WriteLine($"User: {user.Name}");

                    if (user.BankAccounts.Count > 0)
                    {
                        Console.WriteLine("Bank Accounts:");
                        Console.WriteLine(
                            $"Bank Accounts: {Environment.NewLine}{String.Join(Environment.NewLine, user.BankAccounts)}");
                    }

                    if (user.CreditCards.Count > 0)
                    {
                        Console.WriteLine(
                            $"Credit Cards: {Environment.NewLine}{String.Join(Environment.NewLine, user.CreditCards)}");
                    }

                    else
                    {
                        Console.WriteLine($"User with id {userId} not found!");
                    }
                }

                PayBills();
            }
        }


        static void PayBills()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                try
                {
                    Console.WriteLine("------------");

                    Console.WriteLine("Bills Payment");

                    Console.Write("Please enter a user Id: ");

                    var userId = int.Parse(Console.ReadLine());

                    Console.Write("Please enter your bill's amount: ");

                    var amount = decimal.Parse(Console.ReadLine());

                    var accounts = context.PaymentMethods
                        .Include(pm => pm.BankAccount)
                        .Where(pm => pm.UserId == userId && pm.Type == Models.Type.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .ToList();

                    var cards = context.PaymentMethods
                        .Include(pm => pm.CreditCard)
                        .Where(pm => pm.UserId == userId && pm.Type == Models.Type.CreditCard)
                        .Select(pm => pm.CreditCard)
                        .ToList();

                    var moneyAvailable = accounts.Select(ba => ba.Balance).Sum() +
                                         cards.Select(cc => cc.LimitLeft).Sum();

                    if (moneyAvailable < amount)
                    {
                        throw new Exception("Insufficient Funds");
                    }

                    foreach (var account in accounts)
                    {
                        if (amount == 0 || account.Balance == 0)
                        {
                            continue;
                        }

                        var moneyInAccount = account.Balance;
                        if (moneyInAccount < amount)
                        {
                            account.Withdraw(moneyInAccount);
                            amount -= moneyInAccount;
                        }
                        else
                        {
                            account.Withdraw(amount);
                            amount -= amount;
                        }
                    }


                    foreach (var creditCard in cards)
                    {
                        if (amount == 0 || creditCard.LimitLeft == 0)
                        {
                            continue;
                        }

                        var limitLeft = creditCard.LimitLeft;
                        if (limitLeft < amount)
                        {
                            creditCard.Withdraw(limitLeft);
                            amount -= limitLeft;
                        }
                        else
                        {
                            creditCard.Withdraw(amount);
                            amount -= amount;
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        private static void Seed(BillsPaymentSystemContext dbContext)
        {
            var users = Users(dbContext);

            var bankAccounts = BankAccounts(dbContext);

            var creditCards = CreditCards(dbContext);

            GeneratePaymentMethod(dbContext, users, bankAccounts, creditCards);

            dbContext.SaveChanges();
        }

        private static void GeneratePaymentMethod(BillsPaymentSystemContext dbContext, User[] users,
            BankAccount[] bankAccounts,
            CreditCard[] creditCards)
        {
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod
                {
                    User = users[0],
                    Type = Models.Type.BankAccount,
                    BankAccount = bankAccounts[0]
                },

                new PaymentMethod
                {
                    User = users[0],
                    Type = Models.Type.BankAccount,
                    BankAccount = bankAccounts[1]
                },

                new PaymentMethod
                {
                    User = users[0],
                    Type = Models.Type.CreditCard,
                    CreditCard = creditCards[0]
                },

                new PaymentMethod
                {
                    User = users[1],
                    Type = Models.Type.CreditCard,
                    CreditCard = creditCards[1]
                },

                new PaymentMethod
                {
                    User = users[2],
                    Type = Models.Type.BankAccount,
                    BankAccount = bankAccounts[2]
                },

                new PaymentMethod
                {
                    User = users[2],
                    Type = Models.Type.CreditCard,
                    CreditCard = creditCards[2]
                },
            };

            dbContext.PaymentMethods.AddRange(paymentMethods);
        }

        private static CreditCard[] CreditCards(BillsPaymentSystemContext dbContext)
        {
            var creditCards = new CreditCard[]
            {
                new CreditCard(new DateTime(2020, 1, 13), 17000m, 1100m),
                new CreditCard(new DateTime(2021, 2, 25), 20000m, 1200m),
                new CreditCard(new DateTime(2022, 3, 17), 18000m, 14000m),
                new CreditCard(new DateTime(2023, 4, 13), 13000m, 4700m)
            };

            dbContext.CreditCards.AddRange(creditCards);
            return creditCards;
        }

        private static BankAccount[] BankAccounts(BillsPaymentSystemContext dbContext)
        {
            var bankAccounts = new BankAccount[]
            {
                new BankAccount(2455m, "Unicredit Bulbank AD", "UNCRBGSF"),
                new BankAccount(1300m, "ING-DiBa", "INGDDEFFXXX"),
                new BankAccount(1767m, "Deutsche Bank", "DEUTDEMMXXX"),
                new BankAccount(1996m, "Raiffensen bank", "RZBBBGSFXXX")
            };

            dbContext.BankAccounts.AddRange(bankAccounts);
            return bankAccounts;
        }

        private static User[] Users(BillsPaymentSystemContext dbContext)
        {
            var users = new User[]
            {
                new User("Anna", "Winfried", "anna.winfried@gmail.com", "mypassword"),
                new User("Maria", "Marinova", "maria.mar@hotmail.com", "mariasPass"),
                new User("Bill", "Gilbert", "bill.g@gmail.com", "passGil2349"),
                new User("Jonathan", "Johnson", "j.jonhnson@webmail.com", "myStrongPassWorD1234"),
                new User("Tom", "Gilbert", "tom.gilbert@hotmail.com", "myPass123")
            };

            dbContext.Users.AddRange(users);
            return users;
        }
    }
}