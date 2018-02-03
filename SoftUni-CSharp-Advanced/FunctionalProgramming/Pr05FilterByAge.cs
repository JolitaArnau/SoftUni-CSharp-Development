namespace Pr05FilterByAge
{
    using System;
    using System.Collections.Generic;
    
    class Pr05FilterByAge
    {
        static void Main()
        {
            var countPairs = int.Parse(Console.ReadLine());

            var people = new Dictionary<string, int>(countPairs);

            for (int i = 0; i < countPairs; i++)
            {
                var pairArgs = Console.ReadLine().Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries);

                var name = pairArgs[0];
                var age = int.Parse(pairArgs[1]);

              people.Add(name, age);
            }

            var condition = Console.ReadLine();
            var ageFilter = int.Parse(Console.ReadLine());
            var format = Console.ReadLine();

            var filter = GetFilter(condition, ageFilter);
            var printer = CreatePrinter(format);

            Print(people, filter, printer);

        }

        private static void Print(Dictionary<string, int> people, Func<int, bool> filter, Action<KeyValuePair<string, int>> printer)
        {
            foreach (var person in people)
            {
                if (filter(person.Value))
                {
                    printer(person);
                }
            }
        }

        static Func<int, bool> GetFilter(string condition, int ageFilter)
        {
            if (condition.Equals("younger"))
                return x => x < ageFilter;
            else
                return x => x >= ageFilter;
        }

        static Action<KeyValuePair<string, int>> CreatePrinter(string format)
        {
            switch (format)
            {
                case "name":
                    return x => Console.WriteLine(x.Key);
                case "age":
                    return x => Console.WriteLine(x.Value);
                case "name age":
                    return x => Console.WriteLine($"{x.Key} - {x.Value}");
                default: throw new NotImplementedException();
            }
        }
    }
}