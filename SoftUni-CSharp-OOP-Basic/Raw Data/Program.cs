using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        int carsCount = int.Parse(Console.ReadLine());

        var cars = new List<Car>();

        for (int i = 0; i < carsCount; i++)
        {
            var carTokens = Console.ReadLine().Split();


            CreateCar(carTokens, cars);
        }

        var cargoType = Console.ReadLine();

        switch (cargoType)
        {
            case "fragile":
                cars
                    .Where(c => c.Cargo.Type.Equals("fragile") && c.Tires.Any(t => t.Pressure < 1))
                    .ToList()
                    .ForEach(c => Console.WriteLine($"{c.Model}"));
                break;
            case "flamable":
                cars
                    .Where(c => c.Cargo.Type.Equals("flamable") && c.Engine.Power > 250)
                    .ToList()
                    .ForEach(c => Console.WriteLine($"{c.Model}"));
                break;
        }
    }

    private static void CreateCar(string[] carTokens, List<Car> cars)
    {
        var model = carTokens[0];

        // create engine

        var engineSpeed = int.Parse(carTokens[1]);
        var enginePower = int.Parse(carTokens[2]);
        Engine engine = new Engine(engineSpeed, enginePower);

        // create cargo

        var cargoWeight = int.Parse(carTokens[3]);
        var cargoType = carTokens[4];
        Cargo cargo = new Cargo(cargoWeight, cargoType);

        // create tires

        var tireOnePressure = double.Parse(carTokens[5]);
        var tireOneAge = int.Parse(carTokens[6]);
        var tireTwoPressure = double.Parse(carTokens[7]);
        var tireTwoAge = int.Parse(carTokens[8]);
        var tireThreePressure = double.Parse(carTokens[9]);
        var tireThreeAge = int.Parse(carTokens[10]);
        var tireFourPressure = double.Parse(carTokens[11]);
        var tireFourAge = int.Parse(carTokens[12]);
        Tire[] tires = new Tire[4];
        tires[0] = new Tire(tireOnePressure, tireOneAge);
        tires[1] = new Tire(tireTwoPressure, tireTwoAge);
        tires[2] = new Tire(tireThreePressure, tireThreeAge);
        tires[3] = new Tire(tireFourPressure, tireFourAge);

        // create car

        cars.Add(new Car(model, engine, cargo, tires));
    }
}