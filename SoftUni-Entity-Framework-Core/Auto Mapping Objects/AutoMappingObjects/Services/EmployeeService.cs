namespace AutoMappingObjects.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Models;
    using ModelsDto;
    using Contracts;

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesDbContext context;

        public EmployeeService(EmployeesDbContext context)
        {
            this.context = context;
        }

        public Employee ById(int id)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }

            return employee;
        }

        public void Add(string firstName, string lastName, decimal salary)
        {
            var employeeDto = new EmployeeDto {FirstName = firstName, LastName = lastName, Salary = salary};
            var employee = Mapper.Map<Employee>(employeeDto);

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void SetBirthday(int id, DateTime birthday)
        {
            var employee = ById(id);

            employee.Birthday = birthday;

            context.SaveChanges();
        }

        public void SetAddress(int id, string address)
        {
            var employee = ById(id);

            employee.Address = address;

            context.SaveChanges();
        }

        public void SetManager(int emloyeeId, int managerId)
        {
            var employee = ById(emloyeeId);
            var manager = ById(managerId);

            employee.Manager = manager;

            manager.ManagedEmployees.Add(employee);

            context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int id)
        {
            var employee = context.Employees.Find(id);

            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int id)
        {
            var employee = context.Employees.Find(id);

            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }

            var employeePersonalInfoDto = Mapper.Map<EmployeePersonalInfoDto>(employee);

            return employeePersonalInfoDto;
        }

        public ManagerDto GetManagerInfo(int id)
        {
            var manager = ById(id);

            var managerDto = Mapper.Map<ManagerDto>(manager);

            return managerDto;
        }

        public string OlderThan(int age)
        {
            var employees = context.Employees
                .Include(e => e.Manager)
                .Where(e => e.Birthday != null && e.Birthday.Value.Year + age < DateTime.Now.Year)
                .Select(e => Mapper.Map<EmployeeManagerDto>(e))
                .OrderByDescending(e => e.Salary)
                .ToArray();
            
            
            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                var managerLastName = string.Empty;

                if (employee.Manager == null)
                {
                    managerLastName = "[no manager]";
                }
                else
                {
                    managerLastName = employee.Manager.LastName;
                }

                sb.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:F2}" +
                              $" - Manager: {managerLastName}");              
            }

            return sb.ToString();
        }
    }
}