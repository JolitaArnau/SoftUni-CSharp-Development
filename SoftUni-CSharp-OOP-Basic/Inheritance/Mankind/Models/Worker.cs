using System;
using System.Text;

public class Worker : Person
{
    private const string InvalidRange = "Expected value mismatch! Argument:";
    private const int MinWeeklySalary = 10;
    private const int MinWorkingHoursPerDay = 1;
    private const int MaxWorkingHoursPerDay = 12;

    private double weeklySalary;
    private double workingHoursPerDay;

    public Worker(string firstName, string lastName, double weeklySalary, double workingHoursPerDay) : base(firstName,
        lastName)
    {
        WeeklySalary = weeklySalary;
        WorkingHoursPerDay = workingHoursPerDay;
    }

    public double WeeklySalary
    {
        get => weeklySalary;

        private set
        {
            if (value < MinWeeklySalary)
            {
                throw new ArgumentException($"{InvalidRange} weekSalary");
            }

            weeklySalary = value;
        }
    }

    public double WorkingHoursPerDay
    {
        get => workingHoursPerDay;

        private set
        {
            if (value < MinWorkingHoursPerDay || value > MaxWorkingHoursPerDay)
            {
                throw new ArgumentException($"{InvalidRange} workHoursPerDay");
            }

            workingHoursPerDay = value;
        }
    }

    public double CalculateHourlyWage()
    {
        return weeklySalary / (this.workingHoursPerDay * 5);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("First Name: ").AppendLine(FirstName)
            .Append("Last Name: ").AppendLine(LastName)
            .Append("Week Salary: ").AppendLine($"{WeeklySalary:f2}")
            .Append("Hours per day: ").AppendLine($"{WorkingHoursPerDay:f2}")
            .Append("Salary per hour: ").AppendLine($"{CalculateHourlyWage():f2}");

        return sb.ToString();
    }
}