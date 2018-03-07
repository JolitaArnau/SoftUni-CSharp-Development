using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var lineCounter = 0;

        var line = Console.ReadLine();

        var animals = new Queue<Animal>();
        var foods = new Queue<Food>();
        var fedAnimals = new Queue<Animal>();


        while (!line.Equals("End"))
        {
            if (lineCounter == 0 || lineCounter % 2 == 0)
            {
                var animalInfo = line.Split();

                var animalType = animalInfo[0];

                switch (animalType)
                {
                    case "Owl":
                        GenerateOwl(animalInfo, animals);
                        break;
                    case "Hen":
                        GenerateHen(animalInfo, animals);
                        break;
                    case "Mouse":
                        GenerateMouse(animalInfo, animals);
                        break;
                    case "Dog":
                        GenerateDog(animalInfo, animals);
                        break;
                    case "Cat":
                        GenerateCat(animalInfo, animals);
                        break;
                    case "Tiger":
                        GenerateTiger(animalInfo, animals);
                        break;
                }
            }
            else
            {
                var foodInfo = line.Split();
                var foodType = foodInfo[0];
                var foodQuantity = int.Parse(foodInfo[1]);

                switch (foodType)
                {
                    case "Vegetable":
                        GenerateVegetable(foodQuantity, foods);
                        break;
                    case "Fruit":
                        GenerateFruit(foodQuantity, foods);
                        break;
                    case "Meat":
                        GenerateMeat(foodQuantity, foods);
                        break;
                    case "Seeds":
                        GenerateSeeds(foodQuantity, foods);
                        break;
                }
            }

            lineCounter++;
            line = Console.ReadLine();
        }
        
        FeedAnimals(animals, foods, fedAnimals);

        PrintResult(fedAnimals);
    }

    private static void PrintResult(Queue<Animal> fedAnimals)
    {
        foreach (var animal in fedAnimals)
        {
            Console.WriteLine(animal.ToString());
        }
    }

    private static void FeedAnimals(Queue<Animal> animals, Queue<Food> foods, Queue<Animal> fedAnimals)
    {
        while (animals.ToList().Any() && foods.ToList().Any())
        {
            var currentAnimal = animals.Dequeue();
            var currentFood = foods.Dequeue();

            Console.WriteLine(currentAnimal.AskForFood());

            if (currentAnimal.EatsCertainFoodType(currentFood.GetType().Name))
            {
                currentAnimal.GainWeight(currentFood.Quantity);
                currentAnimal.FoodEaten = currentFood.Quantity;
            }
            else
            {
                Console.WriteLine($"{currentAnimal.GetType()} does not eat {currentFood.GetType()}!");
            }

            fedAnimals.Enqueue(currentAnimal);
        }
    }

    private static void GenerateSeeds(int foodQuantity, Queue<Food> foods)
    {
        Food seeds = new Seeds(foodQuantity);

        foods.Enqueue(seeds);
    }

    private static void GenerateFruit(int foodQuantity, Queue<Food> foods)
    {
        Food fruit = new Fruit(foodQuantity);

        foods.Enqueue(fruit);
    }

    private static void GenerateMeat(int foodQuantity, Queue<Food> foods)
    {
        Food meat = new Meat(foodQuantity);

        foods.Enqueue(meat);
    }

    private static void GenerateVegetable(int foodQuantity, Queue<Food> foods)
    {
        Food vegetable = new Vegetable(foodQuantity);

        foods.Enqueue(vegetable);
    }

    private static void GenerateOwl(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var wingSize = double.Parse(animalInfo[3]);

        Animal owl = new Owl(name, weight, wingSize);
        animals.Enqueue(owl);
    }

    private static void GenerateHen(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var wingSize = double.Parse(animalInfo[3]);

        Animal hen = new Hen(name, weight, wingSize);
        animals.Enqueue(hen);
    }

    private static void GenerateMouse(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var livingRegion = animalInfo[3];

        Animal mouse = new Mouse(name, weight, livingRegion);
        animals.Enqueue(mouse);
    }

    private static void GenerateDog(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var livingRegion = animalInfo[3];

        Animal dog = new Dog(name, weight, livingRegion);
        animals.Enqueue(dog);
    }

    private static void GenerateCat(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var livingRegion = animalInfo[3];
        var breed = animalInfo[4];

        Animal cat = new Cat(name, weight, livingRegion, breed);
        animals.Enqueue(cat);
    }

    private static void GenerateTiger(string[] animalInfo, Queue<Animal> animals)
    {
        var name = animalInfo[1];
        var weight = double.Parse(animalInfo[2]);
        var livingRegion = animalInfo[3];
        var breed = animalInfo[4];

        Animal tiger = new Tiger(name, weight, livingRegion, breed);
        animals.Enqueue(tiger);
    }
}