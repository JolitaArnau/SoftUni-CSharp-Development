using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int countDepartments = int.Parse(Console.ReadLine());

        var deparments = new List<Employee>();

        for (int i = 0; i < countDepartments; i++)
        {
            var employeesInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            PopulateDepartmentData(employeesInfo, deparments);
        }

        var departmentWithHighestSalary = deparments
            .GroupBy(e => e.department)
            .Select(e => new
            {
                Department = e.Key,
                AverageSalary = e.Average(emp => emp.salary),
                Employees = e.OrderByDescending(emp => emp.salary)
            })
            .OrderByDescending(e => e.AverageSalary)
            .FirstOrDefault();

        Console.WriteLine($"Highest Average Salary: {departmentWithHighestSalary.Department}");

        foreach (var employee in departmentWithHighestSalary.Employees)
        {
            Console.WriteLine($"{employee.name} {employee.salary:f2} {employee.email} {employee.age}");
        }
    }

    private static void PopulateDepartmentData(string[] employeesInfo, List<Employee> deparments)
    {
        var name = employeesInfo[0];
        var salary = decimal.Parse(employeesInfo[1]);
        var position = employeesInfo[2];
        var department = employeesInfo[3];

        Employee employee = new Employee(name, salary, position, department);

        if (employeesInfo.Length == 5 && employeesInfo[4].Contains("@"))
        {
            var email = employeesInfo[4];
            employee.email = email;
        }
        else if (employeesInfo.Length == 5 && !employeesInfo[4].Contains("@"))
        {
            var age = int.Parse(employeesInfo[4]);
            employee.age = age;
        }

        if (employeesInfo.Length == 6)
        {
            var email = employeesInfo[4];
            var age = int.Parse(employeesInfo[5]);

            employee.email = email;
            employee.age = age;
        }

        deparments.Add(employee);
    }
}