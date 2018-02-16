using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int enginesCount = int.Parse(Console.ReadLine());

        var engines = new List<Engine>();
        
        var cars = new List<Car>();
        
        for (int i = 0; i < enginesCount; i++)
        {
            var engineParams = Console.ReadLine().Split();

            GenerateEngine(engineParams, engines);
        }

        int carsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < carsCount; i++)
        {
            var carParams = Console.ReadLine().Split();
          
            GenerateCar(carParams, engines, cars);
        }

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model}:");
            Console.WriteLine($"    {car.Engine.Model}:");
            Console.WriteLine($"        Power: {car.Engine.Power}");
            Console.WriteLine($"        Displacement: {car.Engine.Displacement}");
            Console.WriteLine($"        Efficiency: {car.Engine.Efficiency}");
            Console.WriteLine($"    Weight: {car.Weight}");
            Console.WriteLine($"    Color: {car.Color}");

        }

        
    }

    private static void GenerateCar(string[] carParams, List<Engine> engines, List<Car> cars)
    {
        var carModel = carParams[0];
        var carEngine = carParams[1];
        
        var engine = engines.FirstOrDefault(e => e.Model.Equals(carEngine));
        
        var car = new Car(carModel, engine);

        if (carParams.Length == 3)
        {
            // weight or color 

            var param = carParams[2];

            var isColor = Char.IsLetter(param[0]);

            if (isColor)
            {
                car.Color = param;
            }
            else
            {
                car.Weight = param;
            }
        }

        if (carParams.Length == 4)
        {
            car.Weight = carParams[2];
            car.Color = carParams[3];
        }
        
        cars.Add(car);
        
    }

    private static void GenerateEngine(string[] engineParams, List<Engine> engines)
    {
        var engineModel = engineParams[0];
        var power = int.Parse(engineParams[1]);
        
        Engine engine = new Engine(engineModel, power);

        if (engineParams.Length == 3)
        {
            // displacement or efficiency

            var param = engineParams[2];

            var isEfficiency = Char.IsLetter(param[0]);

            if (isEfficiency)
            {
                engine.Efficiency = param;
            }
            else
            {
                engine.Displacement = param;
            }
        }

        if (engineParams.Length == 4)
        {
            // displacement and efficiency
            
            engine.Displacement = engineParams[2];
            engine.Efficiency = engineParams[3];
        }

        engines.Add(engine);
    }
}