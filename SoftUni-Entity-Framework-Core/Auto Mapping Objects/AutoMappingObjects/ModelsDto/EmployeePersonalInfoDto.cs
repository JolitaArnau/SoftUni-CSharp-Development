namespace AutoMappingObjects.ModelsDto
{
    using System;
    
    public class EmployeePersonalInfoDto
    {
        private int Id { get; set; }

        private string FirstName { get; set; }

        private string LastName { get; set; }

        private decimal Salary { get; set; }

        private DateTime Birthday { get; set; }

        private string Address { get; set; }
        
        public override string ToString()
        {
            return $"ID: {this.Id} - {this.FirstName} {this.LastName} - ${this.Salary:f2}" +
                   $"{Environment.NewLine}" +
                   $"Birthday: {this.Birthday:dd-MM-yyyy}" +
                   $"{Environment.NewLine}" +
                   $"Address: {this.Address}";
        }
    }
}