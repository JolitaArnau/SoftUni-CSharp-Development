using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var line = Console.ReadLine();

        var ids = new HashSet<string>();

        while (!line.Equals("End"))
        {
            var tryingToPass = line.Split(' ').ToList();
            var id = tryingToPass.Last();
            ids.Add(id);

            line = Console.ReadLine();
        }

        var checker = Console.ReadLine();

        var filteredIds = ids.Where(c => c.EndsWith(checker)).ToHashSet();

        if (filteredIds.Any())
        {
            foreach (var id in filteredIds)
            {
                Console.WriteLine(id);
            }
        }
    }
}