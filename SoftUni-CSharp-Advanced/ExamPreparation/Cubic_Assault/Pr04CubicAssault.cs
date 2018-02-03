using System;
using System.Collections.Generic;
using System.Linq;

namespace Pr04CubicAssault
{
    class Program
    {
        static void Main()
        {
            var statistic = new Dictionary<string, Dictionary<string, long>>();

            var line = Console.ReadLine();

            while (!line.Equals("Count em all"))
            {
                var input = line
                    .Split(new[] {" -> "}, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                PopulateStatistic(input, statistic);

                line = Console.ReadLine();
            }

            PrintStatistic(statistic);
        }

        private static void PrintStatistic(Dictionary<string, Dictionary<string, long>> statistic)
        {

            foreach (var kvp in statistic.OrderByDescending(k => k.Value["Black"]).ThenBy(r => r.Key.Length).ThenBy(r => r.Key))
            {
                Console.WriteLine(kvp.Key);

                foreach (var inner in kvp.Value.OrderByDescending(t => t.Value).ThenBy(t => t.Key))
                {
                    Console.WriteLine($"-> {inner.Key} : {inner.Value}");
                }
            }
        }

        private static void PopulateStatistic(string[] input, Dictionary<string, Dictionary<string, long>> statistic)
        {
            var regionName = input[0];
            var meteorType = input[1];
            var meteorCount = long.Parse(input[2]);

            if (!statistic.ContainsKey(regionName))
            {
                statistic[regionName] = new Dictionary<string, long>();

                statistic[regionName]["Green"] = 0;
                statistic[regionName]["Red"] = 0;
                statistic[regionName]["Black"] = 0;
            }

            statistic[regionName][meteorType] += meteorCount;

            if (statistic[regionName]["Green"] >= 1000000 )
            {
                statistic[regionName]["Red"] += statistic[regionName]["Green"] / 1000000;
                statistic[regionName]["Green"] = statistic[regionName]["Green"] % 1000000;
            }
            
            if (statistic[regionName]["Red"] >= 1000000 )
            {
                statistic[regionName]["Black"] += statistic[regionName]["Red"] / 1000000;
                statistic[regionName]["Red"] = statistic[regionName]["Red"] % 1000000;
            }
        }
    }
}