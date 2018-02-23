using System;

class Program
{
    static void Main()
    {
        var firstName = string.Empty;
        var lastName = string.Empty;

        var studentInfo = Console.ReadLine().Split(' ');
        firstName = studentInfo[0];
        lastName = studentInfo[1];
        var facultyNumber = studentInfo[2];

        Student student;

        try
        {
            student = new Student(firstName, lastName, facultyNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        var workerInfo = Console.ReadLine().Split(' ');
        firstName = workerInfo[0];
        lastName = workerInfo[1];
        var weeklySalary = double.Parse(workerInfo[2]);
        var workHoursPerDay = double.Parse(workerInfo[3]);

        Worker worker;

        try
        {
            worker = new Worker(firstName, lastName, weeklySalary, workHoursPerDay);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine(student.ToString());
        Console.WriteLine(worker.ToString());
    }
}