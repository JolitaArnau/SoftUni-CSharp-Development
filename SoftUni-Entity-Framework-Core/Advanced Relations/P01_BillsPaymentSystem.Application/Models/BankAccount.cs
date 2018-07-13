namespace P01_BillsPaymentSystem.Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BankAccount
    {
        public BankAccount()
        {
        }

        public BankAccount(decimal balance, string bankName, string swiftCode)
        {
            this.Balance = balance;
            this.BankName = bankName;
            this.SWIFTCode = swiftCode;
        }

        [Required]
        public int BankAccountId { get; set; }

        [Required]
        public decimal Balance { get; private set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string SWIFTCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


        public string Withdraw(decimal amount)
        {
            var insufficientBalance = $"You don't have enough balance to withdraw the desired amount of {amount}";

            if (this.Balance - amount < 0 || amount <= 0)
            {
                throw new Exception(insufficientBalance);
            }

            this.Balance -= amount;

            return $"Succesful withdrawal. You're current balance is now {this.Balance}";
        }

        public string Deposit(decimal amount)
        {
            var invalidDepositAmount = $"The amount you're trying to deposit is either zero or negaitve";
            
            if (amount <= 0)
            {
                throw new Exception(invalidDepositAmount);
            }
            
            this.Balance += amount;

            return $"Sucessfully deposited amount {amount}. Your current balance is now {this.Balance}";
        }


        public override string ToString()
        {
            return $"-- ID: {this.BankAccountId}" + Environment.NewLine +
                   $"--- Balance: {this.Balance:f2}" + Environment.NewLine +
                   $"--- Bank: {this.BankName}" + Environment.NewLine +
                   $"--- SWIFT: {this.SWIFTCode}";
        }
    }
}