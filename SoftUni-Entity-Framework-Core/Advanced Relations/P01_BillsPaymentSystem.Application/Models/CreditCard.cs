namespace P01_BillsPaymentSystem.Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        public CreditCard()
        {
        }

        public CreditCard(DateTime expirationDate, decimal limit, decimal moneyOwed)
        {
            this.ExpirationDate = expirationDate;
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
        }


        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        [Required]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int CreditCardId { get; set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwed { get; private set; }

        public void Withdraw(decimal amount)
        {
            var insufficientLimitLeft = $"You don't have enough limit to withdraw the desired amount of {amount}";

            if (this.LimitLeft - amount < 0 || amount <= 0)
            {
                throw new Exception(insufficientLimitLeft);
            }

            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            var invalidDepositAmount = $"The amount you're trying to deposit is either zero or negaitve";

            if (amount <= 0)
            {
                throw new Exception(invalidDepositAmount);
            }

            this.MoneyOwed -= amount;
        }


        public override string ToString()
        {
            return $"-- ID: {this.CreditCardId}" + Environment.NewLine +
                   $"--- Limit: {this.Limit:f2}" + Environment.NewLine +
                   $"--- Money Owed: {this.MoneyOwed:f2}" + Environment.NewLine +
                   $"--- Limit Left: {this.LimitLeft:f2}" + Environment.NewLine +
                   $"--- Expiration Date: {this.ExpirationDate:yyyy/MM}";
        }
    }
}