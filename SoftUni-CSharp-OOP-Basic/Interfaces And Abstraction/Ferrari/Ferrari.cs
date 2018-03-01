using System;

public class Ferrari : ICar
{
    public string Model { get; set; }

    public void Brake()
    {
        Console.Write("Brakes!/");
    }

    public void PushGasPedal()
    {
        Console.Write("Zadu6avam sA!/");
    }

    public string DriverName { get; set; }
}