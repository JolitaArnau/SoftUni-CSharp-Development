﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string StielFieldInfo(string investigatedClass, params string[] requestedFields)
    {
        Type classType = Type.GetType(investigatedClass);
        FieldInfo[] classFields = classType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        Object classInstance = Activator.CreateInstance(classType, new object[] { });

        var sb = new StringBuilder();
        sb.AppendLine($"Class under investigation: {investigatedClass}");

        foreach (FieldInfo field in classFields.Where(f => requestedFields.Contains(f.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
        }
        
        return sb.ToString().Trim();
    }
}