﻿namespace P01_BillsPaymentSystem.Application.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        public User()
        {
        }
        
        public User(string firstName, string lastName, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }
        
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    }
}