using System;

class StartUp
{
    static void Main()
    {
        var line = Console.ReadLine();

        string element;
        int index;

        var myCustomList = new MyList<string>();

        while (!line.Equals("END"))
        {
            var commandArgs = line.Split();

            var action = commandArgs[0];

            switch (action)
            {
                case "Add":
                    element = commandArgs[1];

                    myCustomList.Add(element);
                    break;
                case "Remove":
                    index = int.Parse(commandArgs[1]);

                    myCustomList.Remove(index);
                    break;
                case "Contains":
                    element = commandArgs[1];

                    var containsElement = myCustomList.Contains(element);

                    Console.WriteLine(containsElement ? "True" : "False");
                    break;
                case "Swap":
                    var firstIndex = int.Parse(commandArgs[1]);
                    var secondIndex = int.Parse(commandArgs[2]);

                    myCustomList.Swap(firstIndex, secondIndex);
                    break;
                case "Greater":
                    element = commandArgs[1];

                    var count = myCustomList.CountGraterThan(element);

                    Console.WriteLine(count);
                    break;
                case "Max":
                    var maxElement = myCustomList.Max();

                    Console.WriteLine(maxElement);
                    break;
                case "Min":
                    var minElement = myCustomList.Min();

                    Console.WriteLine(minElement);
                    break;
                case "Sort":
                    myCustomList.Sort();
                    break;
                case "Print":
                    Console.WriteLine(myCustomList.ToString());
                    break;
            }

            line = Console.ReadLine();
        }
    }
}