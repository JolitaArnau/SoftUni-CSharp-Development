using System;
using System.Linq;
using System.Reflection;

public class BlackBoxIntegerTests
{
    public static void Main()
    {
        var classType = typeof(BlackBoxInteger);
        var methodsInfo = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        var blackBoxIntegerInstance = (BlackBoxInteger) Activator.CreateInstance(classType, true);

        var line = Console.ReadLine();
        while (!line.Equals("END"))
        {
            var input = line.Split('_');
            var methodName = input[0];
            var value = int.Parse(input[1]);

            var currentMethod = methodsInfo.FirstOrDefault(m => m.Name.Equals(methodName));

            currentMethod.Invoke(blackBoxIntegerInstance, new object[] {value});

            var innerValue = classType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name.Equals("innerValue"))
                .GetValue(blackBoxIntegerInstance);

            Console.WriteLine(innerValue);

            line = Console.ReadLine();
        }
    }
}