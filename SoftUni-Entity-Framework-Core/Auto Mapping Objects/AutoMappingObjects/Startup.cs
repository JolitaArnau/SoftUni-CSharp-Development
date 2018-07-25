namespace AutoMappingObjects
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Globalization;
    
    using Data;
    using Services;

    public class Startup
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();

            var databaseInitializerService = serviceProvider.GetService<DatabaseInitializerService>();
            databaseInitializerService.InitializeDatabase();

            var employeeService = serviceProvider.GetService<EmployeeService>();

            while (true)
            {
                var input = Console.ReadLine();

                var commandArgs = input.Split();

                var command = commandArgs[0];

                var commandTokens = commandArgs.Skip(1).ToArray();

                if (input.Equals("Exit"))
                {
                    Environment.Exit(0);
                }

                try
                {
                    switch (command)
                    {
                        case "AddEmployee":
                            var firstName = commandTokens[0];
                            var lasttName = commandTokens[1];
                            var salary = decimal.Parse(commandTokens[2]);

                            employeeService.Add(firstName, lasttName, salary);
                            break;
                        case "SetBirthday":
                            var id = int.Parse(commandTokens[0]);
                            var birthday = DateTime.ParseExact(commandTokens[1], "dd-MM-yyyy",
                                CultureInfo.InvariantCulture);

                            employeeService.SetBirthday(id, birthday);
                            break;
                        case "SetAddress":
                            id = int.Parse(commandTokens[0]);
                            var address = commandTokens[1];

                            employeeService.SetAddress(id, address);
                            break;
                        case "EmployeeInfo":
                            id = int.Parse(commandTokens[0]);

                            var employeeDto = employeeService.GetEmployeeInfo(id);

                            Console.WriteLine(employeeDto.ToString());
                            break;
                        case "EmployeePersonalInfo":
                            id = int.Parse(commandTokens[0]);

                            var employeePersonalInfo = employeeService.GetEmployeePersonalInfo(id);

                            Console.WriteLine(employeePersonalInfo.ToString());
                            break;
                        case "SetManager":
                            var employeeId = int.Parse(commandTokens[0]);
                            var managerId = int.Parse(commandTokens[1]);

                            employeeService.SetManager(employeeId, managerId);
                            break;
                        case "ManagerInfo":
                            id = int.Parse(commandTokens[0]);

                            var managerInfo = employeeService.GetManagerInfo(id);

                            Console.WriteLine(managerInfo.ToString());
                            break;
                        case "ListEmployeesOlderThan":
                            var age = int.Parse(commandTokens[0]);

                            var employeesAndManagers = employeeService.OlderThan(age);

                            Console.WriteLine(employeesAndManagers);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesDbContext>(options =>
                options.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<DatabaseInitializerService>();
            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<EmployeesProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}