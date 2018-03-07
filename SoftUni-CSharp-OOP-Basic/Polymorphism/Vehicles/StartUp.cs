using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public const double AirCondiConsumptionCar = 0.9;
    public const double AirCondiConsumptionTruck = 1.6;

    public static void Main()
    {
        double fuelQuantity;
        double fuelConsumption;

        var carTokens = Console.ReadLine().Split();

        fuelQuantity = double.Parse(carTokens[1]);
        fuelConsumption = double.Parse(carTokens[2]);

        Vehicle car = new Car(fuelQuantity, fuelConsumption, AirCondiConsumptionCar);

        var truckTokens = Console.ReadLine().Split();

        fuelQuantity = double.Parse(truckTokens[1]);
        fuelConsumption = double.Parse(truckTokens[2]);

        Vehicle truck = new Truck(fuelQuantity, fuelConsumption, AirCondiConsumptionTruck);

        var commandsCount = int.Parse(Console.ReadLine());


        for (int i = 0; i < commandsCount; i++)
        {
            var commandTokens = Console.ReadLine().Split();
            var command = commandTokens[0];
            var vehicle = commandTokens[1];

            switch (command)
            {
                case "Drive":

                    var distance = double.Parse(commandTokens[2]);

                    if (vehicle.Equals("Car"))
                    {
                        Console.WriteLine(car.Drive(distance, AirCondiConsumptionCar));
                    }
                    else
                    {
                        Console.WriteLine(truck.Drive(distance, AirCondiConsumptionTruck));
                    }

                    break;
                case "Refuel":

                    var refuelLiters = double.Parse(commandTokens[2]);

                    if (vehicle.Equals("Car"))
                    {
                        car.Refuel(refuelLiters);
                    }
                    else
                    {
                        truck.Refuel(refuelLiters);
                    }

                    break;
            }
        }

        Console.WriteLine(car.ToString());

        Console.WriteLine(truck.ToString());
    }
}