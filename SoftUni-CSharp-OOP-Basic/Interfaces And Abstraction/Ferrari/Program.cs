using System;

public class Program
{
    public static void Main()
    {
        var driverName = Console.ReadLine();

        Ferrari ferrari = new Ferrari();

        ferrari.Model = "488-Spider/";
        Console.Write(ferrari.Model);
        ferrari.Brake();
        ferrari.PushGasPedal();
        ferrari.DriverName = driverName;
        Console.Write(ferrari.DriverName);

       

        string ferrariName = typeof(Ferrari).Name;
        string iCarInterfaceName = typeof(ICar).Name;

        bool isCreated = typeof(ICar).IsInterface;
        if (!isCreated)
        {
            throw new Exception("No interface ICar was created");
        }
    }
}