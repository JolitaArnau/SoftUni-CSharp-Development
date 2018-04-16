using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class HarvestingFieldsTest
{
    public static void Main()
    {
        var line = Console.ReadLine();
        while (!line.Equals("HARVEST"))
        {
            var command = line;

            var fields =
                typeof(HarvestingFields).GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                   BindingFlags.NonPublic);

            switch (command)
            {
                case "private":
                    var privateFields = AppendFields(fields.Where(f => f.IsPrivate));
                    PrintFields(privateFields);
                    break;
                case "protected":
                    var protectedFields = AppendFields(fields.Where(f => f.IsFamily));
                    PrintFields(protectedFields);
                    break;
                case "public":
                    var publicFields = AppendFields(fields.Where(f => f.IsPublic));
                    PrintFields(publicFields);
                    break;
                case "all":
                    var allFields = AppendFields(fields);
                    PrintFields(allFields);
                    break;
            }

            line = Console.ReadLine();
        }
    }

    private static void PrintFields(string fieldsToPrint)
    {
        Console.Write(fieldsToPrint);
    }

    private static string AppendFields(IEnumerable<FieldInfo> fields)
    {
        var sb = new StringBuilder();

        foreach (var field in fields)
        {
            var accessmodifier = field.Attributes.ToString().ToLower();
            if (accessmodifier.Equals("family"))
            {
                accessmodifier = "protected";
            }

            sb.AppendLine($"{accessmodifier} {field.FieldType.Name} {field.Name}");
        }

        return sb.ToString();
    }
}