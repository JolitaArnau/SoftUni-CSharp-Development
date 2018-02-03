namespace Exam_Prep
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Pr04_Events
    {
        static void Main()
        {
            var count = int.Parse(Console.ReadLine());

            var register = new SortedDictionary<string, SortedDictionary<string, List<string>>>();

            FillRegister(count, register);

            var locationQuery = Console.ReadLine().Split(',');

            PrintRegister(register, locationQuery);
        }

        private static void PrintRegister(SortedDictionary<string, SortedDictionary<string, List<string>>> register,
            string[] locationQuery)
        {
            foreach (var pair in register)
            {
                if (locationQuery.Contains(pair.Key))
                {
                    Console.WriteLine(pair.Key + ":");
                    int lineNumber = 1;
                    foreach (var personInfo in pair.Value)
                    {
                        var orderedTime = personInfo.Value.OrderBy(t => t);

                        Console.WriteLine($"{lineNumber++}. {personInfo.Key} -> {string.Join(", ", orderedTime)}");
                    }
                }
            }
        }

        private static void FillRegister(int count,
            SortedDictionary<string, SortedDictionary<string, List<string>>> register)
        {
            for (int i = 0; i < count; i++)
            {
                var input = Console.ReadLine();

                Regex pattern = new Regex(@"(#[a-zA-Z:]+)\s+(@[a-zA-Z]+)\s*([0-9]+:[0-9]+)");
                Match match = pattern.Match(input);

                bool isTimeAccurate = false;

                if (match.Success)
                {
                    var timeInput = match.Groups[3].Value.Split(':');
                    isTimeAccurate = int.Parse(timeInput[0]) <= 23 && int.Parse(timeInput[1]) <= 59;
                }

                if (match.Success && isTimeAccurate)
                {
                    //var eventTokens = input.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);


                    var personName = match.Groups[1].Value.TrimStart('#').TrimEnd(':');
                    var locationName = match.Groups[2].Value.TrimStart('@');
                    var time = match.Groups[3].Value;

                    if (!register.ContainsKey(locationName))
                    {
                        register[locationName] = new SortedDictionary<string, List<string>>();

                        if (!register[locationName].ContainsKey(personName))
                        {
                            register[locationName][personName] = new List<string>();
                            register[locationName][personName].Add(time);
                        }
                        else
                        {
                            register[locationName][personName].Add(time);
                        }
                    }
                    else
                    {
                        if (!register[locationName].ContainsKey(personName))
                        {
                            register[locationName][personName] = new List<string>();
                            register[locationName][personName].Add(time);
                        }
                        else
                        {
                            register[locationName][personName].Add(time);
                        }
                    }
                }
            }
        }
    }
}