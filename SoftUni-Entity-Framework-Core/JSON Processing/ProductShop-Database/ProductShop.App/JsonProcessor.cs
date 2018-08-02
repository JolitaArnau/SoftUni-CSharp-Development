namespace ProductShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    
    using Data;
    using Models;
    
    public class JsonProcessor
    {
        public JsonProcessor()
        {
            this.Context = new ProductShopContext();
        }

        private ProductShopContext Context { get; set; }

        public void SeedData(ProductShopContext context)
        {
            ImportUsers();

            ImportCategories();

            ImportProducts();

            CreateCategoryProducts();
        }

        public void ExportData()
        {
            GetProductsInRange();
            
            GetSoldProducts();
            
            GetCategoriesByProductsCount();

            GetUsersAndProducts();
        }

        private void ImportUsers()
        {
            var json = File.ReadAllText("Json/users.json");

            var users = JsonConvert.DeserializeObject<User[]>(json);

            this.Context.Users.AddRange(users);

            this.Context.SaveChanges();
        }

        private void ImportCategories()
        {
            var json = File.ReadAllText("Json/categories.json");

            var categories = JsonConvert.DeserializeObject<Category[]>(json);

            this.Context.Categories.AddRange(categories);

            this.Context.SaveChanges();
        }

        private void ImportProducts()
        {
            var json = File.ReadAllText("Json/products.json");

            var products = JsonConvert.DeserializeObject<Product[]>(json);

            var users = this.Context.Users.ToArray();

            var random = new Random();

            foreach (var product in products)
            {
                var seller = users[random.Next(0, users.Length)];
                product.Seller = seller;

                var buyerId = random.Next(0, users.Length + (int) (users.Length * 0.3));
                product.Buyer = buyerId < users.Length ? users[buyerId] : null;
            }

            this.Context.Products.AddRange(products);

            this.Context.SaveChanges();
        }


        private void CreateCategoryProducts()
        {
            var categoryProducts = new List<CategoryProduct>();
            var products = this.Context.Products.ToArray();
            var categories = this.Context.Categories.ToArray();
            var random = new Random();

            foreach (var t in products)
            {
                var categoriesCount = random.Next(1, 4);
                var currentCategories = new HashSet<Category>();

                for (var j = 1; j <= categoriesCount; j++)
                {
                    var category = categories[random.Next(0, categories.Length)];
                    currentCategories.Add(category);
                }

                currentCategories.ToList().ForEach(c =>
                    categoryProducts.Add(new CategoryProduct {Category = c, Product = t}));
            }

            this.Context.CategoryProducts.AddRange(categoryProducts);

            this.Context.SaveChanges();
        }

        private void GetProductsInRange()
        {
            var products = this.Context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(e => new
                {
                    name = e.Name,
                    price = e.Price,
                    seller = $"{e.Seller.FirstName} {e.Seller.LastName}" ?? e.Seller.LastName
                })
                .ToArray();
            
            var jsonSerializerSettings = DefaultNullValueHandling();

            var dataAsJson = JsonConvert.SerializeObject(products, Formatting.Indented, jsonSerializerSettings);
            
            File.WriteAllText("Output/products-in-range.json", dataAsJson);
        }

        private void GetSoldProducts()
        {
            var products = this.Context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(e => new
                {
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    soldProducts = e.ProductsSold.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName,
                    }).ToArray()
                })
                .ToArray();

            var jsonSerializerSettings = DefaultNullValueHandling();

            var dataAsJson = JsonConvert.SerializeObject(products, Formatting.Indented, jsonSerializerSettings);
            
            File.WriteAllText("Output/users-sold-products.json", dataAsJson);
        }

        private void GetCategoriesByProductsCount()
        {
            var categories = this.Context.CategoryProducts
                .OrderBy(c => c.Category.CategoryProducts.Count)
                .Select(c => new
                {
                    name = c.Category.Name,
                    productsCount = c.Category.CategoryProducts.Count,
                    averagePrice = c.Category.CategoryProducts.Average(p => p.Product.Price),
                    totalRevenue = c.Category.CategoryProducts.Sum(p => p.Product.Price)
                }).ToArray();

            var jsonSerializerSettings = DefaultNullValueHandling();

            var dataAsJson = JsonConvert.SerializeObject(categories, Formatting.Indented, jsonSerializerSettings);
            
            File.WriteAllText("Output/categories-by-products.json", dataAsJson);
        }

        private void GetUsersAndProducts()
        {
            var users = this.Context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(e => new
                {
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    age = e.Age,
                    soldProducts = new
                    {
                       count = e.ProductsSold.Count,
                       products = e.ProductsSold.Select(ps => new
                       {
                           name = ps.Name,
                           price = ps.Price
                       }).ToArray()
                    }
                }).ToArray();

            var usersAndProducts = new
            {
                usersCount = users.Count(),
                users = users
            };

            var jsonSerializerSettings = DefaultNullValueHandling();

            var dataAsJson = JsonConvert.SerializeObject(usersAndProducts, Formatting.Indented, jsonSerializerSettings);

            File.WriteAllText("Output/users-and-products.json", dataAsJson);
        }

        private JsonSerializerSettings DefaultNullValueHandling()
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };

            return settings;
        }
    }
}