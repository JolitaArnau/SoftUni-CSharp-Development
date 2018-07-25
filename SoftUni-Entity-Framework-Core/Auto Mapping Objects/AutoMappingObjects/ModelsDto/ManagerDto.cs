namespace AutoMappingObjects.ModelsDto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ManagerDto
    {
        private string FirstName { get; set; }

        private string LastName { get; set; }

        private List<EmployeeDto> ManagedEmployees { get; set; }

        private int EmployeesCount => this.ManagedEmployees.Count;

        public override string ToString()
        {
            return $"{FirstName} {LastName} | Employees: {EmployeesCount}" +
                   $"{Environment.NewLine}" +
                   $"{string.Join("- ", ManagedEmployees.Select(me => me.FirstName + " " + me.LastName + " - " + me.Salary))}";
        }
    }
}