using System;

public class Truck : Vehicle
{
    public Truck(double fuelQuantity, double consumptionPerKm, double airConditioningConsumption) : base(fuelQuantity,
        consumptionPerKm, airConditioningConsumption)
    {
    }

    public override void Refuel(double refuelLiters)
    {
        FuelQuantity *= 0.95;
        base.Refuel(refuelLiters);
    }
}