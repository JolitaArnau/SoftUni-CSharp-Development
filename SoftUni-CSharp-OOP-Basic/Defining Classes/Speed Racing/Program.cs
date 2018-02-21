using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int carsCount = int.Parse(Console.ReadLine());

        var cars = new List<Car>();

        for (int i = 0; i < carsCount; i++)
        {
            var carParams = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Car car = new Car()
            {
                Model = carParams[0],
                FuelAmount = decimal.Parse(carParams[1]),
                FuelConsumptionPerKm = decimal.Parse(carParams[2]),
                DistanceTraveled = 0
            };

            cars.Add(car);
        }

        var line = Console.ReadLine();

        while (!line.Equals("End"))
        {
            var commandTokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var carModel = commandTokens[1];
            var distanceToDrive = decimal.Parse(commandTokens[2]);
            
            var carToDrive = cars.FirstOrDefault(c => c.Model.Equals(carModel));
            
            carToDrive.Drive(distanceToDrive);

            line = Console.ReadLine();
        }
        
        cars.ForEach(c => Console.WriteLine($"{c.Model} {c.FuelAmount:f2} {c.DistanceTraveled}"));
    }
}