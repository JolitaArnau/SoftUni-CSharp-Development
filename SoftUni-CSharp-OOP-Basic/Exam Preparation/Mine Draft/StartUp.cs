using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var draftManager = new DraftManager();

        var line = Console.ReadLine();

        while (true)
        {
            if (line.Equals("Shutdown"))
            {
                Console.WriteLine(draftManager.ShutDown());
                
                break;
            }

            var arguments = line.Split(' ').ToList();
            var command = arguments[0];
            arguments.RemoveAt(0);


            switch (command)
            {
                case "RegisterHarvester":
                    Console.WriteLine(draftManager.RegisterHarvester(arguments));
                    break;
                case "RegisterProvider":
                    Console.WriteLine(draftManager.RegisterProvider(arguments));
                    break;
                case "Day":
                    Console.WriteLine(draftManager.Day());
                    break;
                case "Mode":
                    Console.WriteLine(draftManager.Mode(arguments));
                    break;
                case "Check":
                    Console.WriteLine(draftManager.Check(arguments));
                    break;
            }

            line = Console.ReadLine();
        }
    }
}