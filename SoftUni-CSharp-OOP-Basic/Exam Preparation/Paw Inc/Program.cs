namespace PawInc
{
    using System;
    using System.Linq;
    using PawInc.Controllers;
    
    class Program
    {
        static void Main()
        {
            var line = Console.ReadLine();

            var pawIncManager = new PawIncManager();

            var adoptionCenterName = string.Empty;
            var cleansingCenterName = string.Empty;

            while (true)
            {
                if (line.Equals("Paw Paw Pawah"))
                {
                    Console.WriteLine(pawIncManager.Report());
                    break;
                }

                var arguments = line.Split(new string[] {" | "}, StringSplitOptions.RemoveEmptyEntries).ToList();
                var command = arguments[0];
                arguments.RemoveAt(0);

                switch (command)
                {
                    case "RegisterAdoptionCenter":
                        adoptionCenterName = arguments[0];
                        pawIncManager.RegisterAdoptionCenter(adoptionCenterName);
                        break;
                    case "RegisterCleansingCenter":
                        cleansingCenterName = arguments[0];
                        pawIncManager.RegisterCleansingCenter(cleansingCenterName);
                        break;
                    case "RegisterDog":
                        pawIncManager.RegisterDog(arguments);
                        break;
                    case "RegisterCat":
                        pawIncManager.RegisterCat(arguments);
                        break;
                    case "SendForCleansing":
                        pawIncManager.SendForCleansing(arguments);
                        break;
                    case "Cleanse":
                        cleansingCenterName = arguments[0];
                        pawIncManager.Cleanse(cleansingCenterName);
                        break;
                    case "Adopt":
                        adoptionCenterName = arguments[0];
                        pawIncManager.Adopt(adoptionCenterName);
                        break;
                }


                line = Console.ReadLine();
            }
        }
    }
}