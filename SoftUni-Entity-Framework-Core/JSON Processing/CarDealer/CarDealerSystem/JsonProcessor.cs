using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.App
{
    using System.IO;
    using Newtonsoft.Json;
    using Data;
    using Models;

    public class JsonProcessor
    {
        private CarDealerContext Context { get; set; }

        public JsonProcessor()
        {
            this.Context = new CarDealerContext();
        }

        public void ExportData(CarDealerContext context)
        {
            GetOrderedCustomers();

            GetToyotaCars();

            GetLocalSuppliers();

            GetCarsAndParts();

            GetCustomersTotalSales();

            GetSalesWithDiscounts();
        }

        public void SeedData(CarDealerContext context)
        {
            ImportSuppliers();

            ImportParts();

            ImportCars();

            GenerateCarParts();

            ImportCustomers();

            GenerateSales();
        }


        private void ImportSuppliers()
        {
            var json = File.ReadAllText("Resources/suppliers.json");

            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(json);

            this.Context.Suppliers.AddRange(suppliers);

            this.Context.SaveChanges();
        }

        private void ImportParts()
        {
            var json = File.ReadAllText("Resources/parts.json");

            var deserializedParts = JsonConvert.DeserializeObject<Part[]>(json);

            var random = new Random();

            var parts = new List<Part>();

            foreach (var part in deserializedParts)
            {
                var supplierId = random.Next(1, this.Context.Suppliers.Count() + 1);

                part.SupplierId = supplierId;

                parts.Add(part);
            }

            this.Context.Parts.AddRange(parts);

            this.Context.SaveChanges();
        }

        private void ImportCars()
        {
            var json = File.ReadAllText("Resources/cars.json");

            var cars = JsonConvert.DeserializeObject<Car[]>(json);

            this.Context.Cars.AddRange(cars);

            this.Context.SaveChanges();
        }

        private void GenerateCarParts()
        {
            var partCars = new List<PartCars>();

            var random = new Random();

            var partMaxRandom = this.Context.Parts.Count() + 1;

            for (var carId = 1; carId <= this.Context.Cars.Count(); carId++)
            {
                var countOfPartsToAdd = random.Next(10, 20);

                var addedPartsIds = new List<int>();

                for (var partCount = 0; partCount < countOfPartsToAdd; partCount++)
                {
                    var partId = random.Next(1, partMaxRandom);

                    if (addedPartsIds.Contains(partId))
                    {
                        partCount--;
                        continue;
                    }

                    addedPartsIds.Add(partId);

                    var partCar = new PartCars
                    {
                        CarId = carId,
                        PartId = partId,
                    };

                    partCars.Add(partCar);
                }
            }

            this.Context.PartCars.AddRange(partCars);

            this.Context.SaveChanges();
        }

        private void ImportCustomers()
        {
            var json = File.ReadAllText("Resources/customers.json");

            var customers = JsonConvert.DeserializeObject<Customer[]>(json);

            this.Context.Customers.AddRange(customers);

            this.Context.SaveChanges();
        }

        private void GenerateSales()
        {
            var discounts = new int[] {0, 5, 10, 15, 20, 30, 40, 50};

            var youngDriverDiscount = 5;

            var sales = new List<Sale>();

            var customersWithCarIds = new List<int>();

            var soldCarsIds = new List<int>();

            var random = new Random();

            var carIdMaxRnd = this.Context.Cars.Count() + 1;

            for (var i = 0; i < 150; i++)
            {
                var customerId = random.Next(1, this.Context.Customers.Count() + 1);

                var carId = random.Next(1, carIdMaxRnd);

                if (customersWithCarIds.Contains(customerId) && soldCarsIds.Contains(carId))
                {
                    i--;
                    continue;
                }

                customersWithCarIds.Add(customerId);

                soldCarsIds.Add(carId);

                var discount = discounts[random.Next(8)];

                var isYoungDriver = this.Context.Customers
                    .FirstOrDefault(c => c.Id == customerId)
                    .IsYoungDriver;

                if (isYoungDriver)
                {
                    discount += youngDriverDiscount;
                }

                var sale = new Sale
                {
                    Discount = discount,
                    CustomerId = customerId,
                    CarId = carId,
                };

                sales.Add(sale);
            }

            this.Context.Sales.AddRange(sales);

            this.Context.SaveChanges();
        }

        private void GetOrderedCustomers()
        {
            var customers = this.Context.Customers
                .OrderBy(c => c.BirthDate)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("Output/ordered-customers.json", jsonString);
        }

        private void GetToyotaCars()
        {
            var toyotas = this.Context.Cars
                .Where(c => c.Make.Equals("Toyota"))
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(toyotas, Formatting.Indented);

            File.WriteAllText("Output/toyota-cars.json", jsonString);
        }

        private void GetLocalSuppliers()
        {
            var localSuppliers = this.Context.Suppliers
                .Where(ls => !ls.IsImporter)
                .Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    PartsCount = e.Parts.Count
                }).ToArray();

            var jsonString = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);

            File.WriteAllText("Output/local-suppliers.json", jsonString);
        }

        private void GetCarsAndParts()
        {
            var carsAndParts = this.Context.PartCars
                .Select(e => new
                {
                    Make = e.Car.Make,
                    Model = e.Car.Model,
                    TravelledDistance = e.Car.TravelledDistance,
                    parts = e.Car.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    }).ToArray()
                }).ToArray();

            var jsonString = JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);

            File.WriteAllText("Output/cars-and-parts.json", jsonString);
        }

        private void GetCustomersTotalSales()
        {
            var customersSales = this.Context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(e => new
                {
                    fullName = e.Name,
                    boughtCars = e.Sales.Count,
                    spentMoney = Math.Round(e.Sales
                        .Select(s => s.Car.PartCars.Sum(p => p.Part.Price) * ((100m - s.Discount) / 100m))
                        .DefaultIfEmpty(0)
                        .Sum(), 2),
                })
                .OrderByDescending(p => p.spentMoney)
                .ThenByDescending(p => p.boughtCars)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(customersSales, Formatting.Indented);

            File.WriteAllText("Output/customers-total-sales.json", jsonString);
        }

        private void GetSalesWithDiscounts()
        {
            var sales = this.Context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance,
                    },

                    customerName = s.Customer.Name,
                    Discount = Math.Round((s.Discount / 100m), 2, MidpointRounding.AwayFromZero),
                    price = Math.Round(
                        s.Car.PartCars.Select(p => p.Part.Price * ((100m - s.Discount) / 100m)).DefaultIfEmpty(0).Sum(),
                        2),
                    priceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price),
                }).ToArray();
            
            var jsonString = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText("Output/sales-discounts.json", jsonString);
        }
    }
}