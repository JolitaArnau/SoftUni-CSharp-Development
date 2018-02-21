using System;

public class Car
{
    public string Model { get; set; }
    
    public decimal FuelAmount { get; set; }
    
    public decimal FuelConsumptionPerKm { get; set; }
    
    public decimal DistanceTraveled { get; set; }

    public void Drive(decimal distanceToDrive)
    {
        if (distanceToDrive <= FuelAmount / FuelConsumptionPerKm)
        {
            DistanceTraveled += distanceToDrive;
            FuelAmount -= FuelConsumptionPerKm * distanceToDrive;
        }
        else
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
    }
}