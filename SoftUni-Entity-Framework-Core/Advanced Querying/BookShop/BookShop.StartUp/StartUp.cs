namespace BookShop
{
    using Data;
    using System;
    using System.Linq;
    using Models;
    using System.Text;
    using Initializer;
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);

                var command = Console.ReadLine();

                //var year = int.Parse(Console.ReadLine());

                //var lenght = int.Parse(Console.ReadLine());

                Console.WriteLine(GetBooksByCategory(db, command));
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var commandType = (AgeRestriction) Enum.Parse(typeof(AgeRestriction), CapitalizeCommand(command));

            var sb = new StringBuilder();

            context.Books
                .Where(b => b.AgeRestriction == commandType)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            context
                .Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var sb = new StringBuilder();

            context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            
            context.Books
                .Where(b => b.BookCategories
                    .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList()
                .ForEach(t => sb.AppendLine(t));


            return sb.ToString();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var sb = new StringBuilder();

            var parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            context
                .Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList()
                .ForEach(t => sb.AppendLine($"{t.Title} - {t.EditionType} - ${t.Price:f2}"));

            return sb.ToString();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList()
                .ForEach(t => sb.AppendLine(t));

            return sb.ToString();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(ba => new
                {
                    BookTitle = ba.Title,
                    FormatedAuthorName = $"({ba.Author.FirstName} {ba.Author.LastName})"
                })
                .ToList()
                .ForEach(t => sb.AppendLine($"{t.BookTitle} {t.FormatedAuthorName}"));


            return sb.ToString();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCounts = context
                .Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCounts;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToList()
                .ForEach(t => sb.AppendLine($"{t.Name} - {t.Copies}"));

            return sb.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            context.Categories
                .Select(ca => new
                {
                    CategoryName = ca.Name,
                    Profit = ca.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(p => p.Profit)
                .ThenBy(c => c.CategoryName)
                .ToList()
                .ForEach(t => sb.AppendLine($"{t.CategoryName} ${t.Profit:f2}"));

            return sb.ToString();
        }


        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var resultSet = context.Categories
                .Select(ca => new
                {
                    CategoryName = ca.Name,
                    RecentBooks = ca.CategoryBooks.Select(b => b.Book).OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                })
                .OrderBy(c => c.CategoryName)
                .ToList();


            return String.Join(Environment.NewLine,
                resultSet.Select(c => $"--{c.CategoryName}" +
                                      $"{Environment.NewLine}" +
                                      $"{String.Join(Environment.NewLine, c.RecentBooks.Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))}"));
        }
        
        public static void IncreasePrices(BookShopContext context)
        {
            context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(p => p.Price += 5);

            context.SaveChanges();
        }
        
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToDelete = context
                .Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var countOfBooks = booksToDelete.Count;

            context.Books.RemoveRange(booksToDelete);

            context.SaveChanges();

            return countOfBooks;
        }

        private static string CapitalizeCommand(string command)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            command = textInfo.ToTitleCase(command.ToLower());
            return command;
        }
    }
}