using System;
using System.Collections.Generic;
using System.Linq;
using IntroToEFCore.Data;
using IntroToEFCore.Data.Models;

namespace IntroToEFCore
{
    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            var dateFormat = "M/d/yyyy h:mm:ss tt";

            // 03. Employees Full Information

            var employeesFullInfo = dbContext.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");


            Console.WriteLine(string.Join(Environment.NewLine, employeesFullInfo));

            // 04. Employees with Salary Over 50 000

            var employeesWithSalaryAboveFiftyThousand = dbContext.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(fn => fn.FirstName)
                .Select(e => e.FirstName);

            foreach (var employeeName in employeesWithSalaryAboveFiftyThousand)
            {
                Console.WriteLine(employeeName); 
            }

            // 05. Employees from Research and Development 

            var employeesResearchAndDevDept = dbContext.Employees
                .Where(dpName => dpName.Department.Name == "Research And Development")
                .OrderBy(s => s.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => $"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");

            Console.WriteLine(string.Join(Environment.NewLine, employeesResearchAndDevDept));

            // 06. Adding a New Address and Updating Employee

            var employeeNamedNakov = dbContext
                .Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employeeNamedNakov.Address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            dbContext.SaveChanges();

            var adresses = dbContext.Employees
                .OrderByDescending(a => a.AddressId)
                .Take(10)
                .Select(a => $"{a.Address.AddressText}");

            Console.WriteLine(string.Join(Environment.NewLine, adresses));

            // 07. Employees and Projects 


            var employeesProjects = dbContext.Employees
                .Where(e => e.EmployeesProjects
                    .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(30)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                        .Select(ep => ep.Project)
                })
                .ToList();

            employeesProjects.ForEach(e => Console.WriteLine(
                ($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}{Environment.NewLine}" +
                 $"{string.Join(Environment.NewLine, e.Projects.Select(p => $"--{p.Name} - {p.StartDate.ToString(dateFormat)} - {(p.EndDate == null ? "not finished" : p.EndDate?.ToString(dateFormat))}"))} "
                )));

            // 08. Addresses By Town

            var addressesByTown = dbContext.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(t => t.Town.Name)
                .ThenBy(at => at.AddressText)
                .Take(10)
                .Select(r => $"{r.AddressText}, {r.Town.Name} - {r.Employees.Count} employees");

            Console.WriteLine(string.Join(Environment.NewLine, addressesByTown));

            // 09. Employee 147

            var employeeWithIdOneHundredFourthySeven = dbContext.Employees
                .Where(e => e.EmployeeId == 147)
                .OrderBy(s => s)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .Select(ep => ep.Project.Name)
                        .OrderBy(p => p)
                        .ToList()
                })
                .First();

            Console.WriteLine(
                $"{employeeWithIdOneHundredFourthySeven.FirstName} {employeeWithIdOneHundredFourthySeven.LastName} - " +
                $"{employeeWithIdOneHundredFourthySeven.JobTitle}{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine, employeeWithIdOneHundredFourthySeven.Projects)}");

            // 10. Departments with More Than 5 Employees

            var departments = dbContext.Departments
                .Where(ec => ec.Employees.Count > 5)
                .OrderBy(ec => ec.Employees.Count)
                .ThenBy(dp => dp.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFullName = d.Manager.FirstName + " " + d.Manager.LastName,
                    EmployeeInfo = d.Employees.Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle}")
                        .OrderBy(n => n)
                }).ToList();

            departments.ForEach(d => Console.WriteLine($"{d.DepartmentName} - {d.ManagerFullName}" +
                                                       $"{Environment.NewLine}" +
                                                       $"{string.Join(Environment.NewLine, d.EmployeeInfo)}" +
                                                       $"{Environment.NewLine}" +
                                                       "----------"));

            // 11. Find Latest 10 Projects

            var latestProjects = dbContext.Projects
                .OrderByDescending(d => d.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p =>
                    $"{p.Name}" +
                    $"{Environment.NewLine}" +
                    $"{p.Description}" +
                    $"{Environment.NewLine}" +
                    $"{p.StartDate.ToString(dateFormat)}");

            Console.WriteLine(string.Join(Environment.NewLine, latestProjects));

            // 12. Increase Salaries

            var departmentsToUpdate = dbContext.Employees
                .Where(d => d.Department.Name == "Engineering" ||
                            d.Department.Name == "Tool Design" ||
                            d.Department.Name == "Marketing" ||
                            d.Department.Name == "Information Services").AsQueryable();

            foreach (var employee in departmentsToUpdate.ToList())
            {
                employee.Salary /= (decimal) 1.12;
            }

            dbContext.SaveChanges();

            var test = departmentsToUpdate
                .OrderBy(n => n.FirstName)
                .ThenBy(n => n.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:f2})").ToList();

            Console.WriteLine(string.Join(Environment.NewLine, test));

            // 13. Find Employees by First Name Starting With "Sa"

            var employeesStartingWithSa = dbContext.Employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Where(n => n.FirstName.StartsWith("Sa"))
                .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})")
                .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, employeesStartingWithSa));

            // 14. Delete Project by Id

            var projectToDelete = dbContext.Projects.Find(2);

            dbContext.EmployeesProjects.ToList().ForEach(ep => dbContext.EmployeesProjects.Remove(ep));
            dbContext.Projects.Remove(projectToDelete);

            dbContext.SaveChanges();

            dbContext.Projects.Take(10).Select(p => p.Name).ToList().ForEach(p => Console.WriteLine(p));

        }
    }
}