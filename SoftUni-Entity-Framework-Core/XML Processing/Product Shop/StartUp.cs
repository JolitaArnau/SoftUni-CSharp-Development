namespace Product_Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Models;
    using Data;

    public class StartUp
    {
        public static void Main()
        {
            var context = new ProductShopContext();

            using (context)
            {
                context.Database.EnsureDeleted();

                context.Database.EnsureCreated();

                PopulateUsers(context);

                PopulateCategories(context);

                PopulateProducts(context);

                ProductsInRange(context);

                SoldProducts(context);

                UsersAndProductsXML(context);
            }
        }

        private static void PopulateUsers(ProductShopContext context)
        {
            var xmlDoc = XDocument.Load("Resources/users.xml");

            var elements = xmlDoc.Root.Elements();

            var users = new List<User>();

            foreach (var user in elements)
            {
                var firstName = user.Attribute("firstName")?.Value;
                var lastName = user.Attribute("lastName")?.Value;
                int? age = null;


                if (user.Attribute("age") != null)
                {
                    age = int.Parse(user.Attribute("age").Value);
                }

                users.Add(new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                });
            }

            context.Users.AddRange(users);

            context.SaveChanges();
        }

        private static void PopulateCategories(ProductShopContext context)
        {
            var xmlDoc = XDocument.Load("Resources/categories.xml");

            var elements = xmlDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var element in elements)
            {
                var categoryName = element.Element("name").Value;

                categories.Add(new Category()
                {
                    Name = categoryName
                });
            }

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        private static void PopulateProducts(ProductShopContext context)
        {
            XDocument xDoc = XDocument.Load("Resources/products.xml");
            var elements = xDoc.Root.Elements();

            var products = new List<Product>();
            var users = context.Users.ToArray();
            var rnd = new Random();

            foreach (var element in elements)
            {
                var name = element.Element("name").Value;
                var price = decimal.Parse(element.Element("price").Value);

                Product product = new Product
                {
                    Name = name,
                    Price = price
                };

                var seller = users[rnd.Next(0, users.Length)];
                product.Seller = seller;

                var buyerId = rnd.Next(0, users.Length + (int) (users.Length * 0.3));
                product.Buyer = buyerId < users.Length ? users[buyerId] : null;

                products.Add(product);
            }

            context.Products.AddRange(products);

            context.SaveChanges();
        }

        private static void ProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .ToList();

            var xDoc = new XDocument();

            xDoc.Add(new XElement("products"));

            foreach (var product in products)
            {
                xDoc.Element("products")
                    .Add(new XElement("product", new XAttribute("name", product.name),
                        new XAttribute("price", product.price), new XAttribute("buyer", product.buyer)));
            }

            xDoc.Save("Output/ProductsInRange.xml");
        }

        private static void SoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.SoldProducts.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.SoldProducts
                        .Select(sp => new
                        {
                            name = sp.Name,
                            price = sp.Price
                        })
                });

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach(var u in users)
            {
                var user = new XElement("user");
                user.SetAttributeValue("first-name", u.FirstName);
                user.SetAttributeValue("last-name", u.LastName);

                var products = new XElement("sold-products");

                foreach(var p in u.SoldProducts)
                {
                    var product = new XElement("product");
                    product.Add(new XElement("name", p.name), new XElement("price", p.price));
                    products.Add(product);
                }

                user.Add(products);

                xDoc.Element("users").Add(user);
            }

            xDoc.Save("Output/SoldProducts.xml");
        }
        
        private static void UsersAndProductsXML(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.SoldProducts.Count > 0)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.SoldProducts.Count,
                        products = u.SoldProducts
                            .Select(sp => new
                            {
                                name = sp.Name,
                                price = sp.Price
                            })
                            .ToArray()
                    }
                })
                .OrderByDescending(o => o.soldProducts.count)
                .ThenBy(o => o.lastName)
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));
            xDoc.Element("users").SetAttributeValue("count", users.Count());

            foreach(var u in users)
            {
                var user = new XElement("user");
                user.SetAttributeValue("first-name", u.firstName);
                user.SetAttributeValue("last-name", u.lastName);
                user.SetAttributeValue("age", u.age);

                var soldProducts = new XElement("sold-products");
                soldProducts.SetAttributeValue("count", u.soldProducts.count);

                foreach(var p in u.soldProducts.products)
                {
                    var product = new XElement("product", new XAttribute("name", p.name), new XAttribute("price", p.price));

                    soldProducts.Add(product);
                }

                user.Add(soldProducts);

                xDoc.Element("users").Add(user);
            }

            xDoc.Save("Output/UsersAndProducts.xml");
        }

    }
}