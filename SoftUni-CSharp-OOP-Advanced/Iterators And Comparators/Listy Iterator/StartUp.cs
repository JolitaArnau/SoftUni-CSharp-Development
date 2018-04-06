using System;
using System.Linq;
using ListyIterator;

public class StartUp
{
    public static void Main()
    {
        Iterator<string> collection = new Iterator<string>();

        var line = Console.ReadLine();
        while (!line.Equals("END"))
        {
            var commandArgs = line.Split().ToList();
            var command = commandArgs[0];

            try
            {
                switch (command)
                {
                    case "Create":
                        commandArgs.Remove(command);
                        var sequence = commandArgs;
                        collection = new Iterator<string>(sequence);
                        break;
                    case "Move":
                        Console.WriteLine(collection.Move() ? "True" : "False");
                        break;
                    case "Print":
                        Console.WriteLine(collection.Print());
                        break;
                    case "HasNext":
                        Console.WriteLine(collection.HasNext() ? "True" : "False");
                        break;
                    case "PrintAll":
                        Console.WriteLine(collection.ToString());
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            line = Console.ReadLine();
        }
    }
}