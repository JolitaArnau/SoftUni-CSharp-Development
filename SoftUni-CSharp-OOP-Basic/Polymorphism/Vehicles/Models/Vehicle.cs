using System;

public abstract class Vehicle
{
    private double fuelQuantity;
    private double consumptionPerKm;
    private double airConditioningConsumption;

    public Vehicle(double fuelQuantity, double consumptionPerKm, double airConditioningConsumption)
    {
        FuelQuantity = fuelQuantity;
        ConsumptionPerKm = consumptionPerKm;
    }

    public double FuelQuantity { get; set; }

    public double ConsumptionPerKm { get; set; }

    public string Drive(double distance, double airConditioningConsumption)
    {
        var fuelAmountNeeded = distance * (ConsumptionPerKm + airConditioningConsumption);

        if (fuelAmountNeeded > FuelQuantity)
        {
            return $"{GetType().Name} needs refueling";
        }

        FuelQuantity -= fuelAmountNeeded;
        return $"{GetType().Name} travelled {distance} km";
    }

    public virtual void Refuel(double refuelLiters)
    {
        FuelQuantity += refuelLiters;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {FuelQuantity:f2}";
    }
}