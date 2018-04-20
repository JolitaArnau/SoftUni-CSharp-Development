using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ProviderFactory : IProviderFactory
{
    private const string baseClassName = "Provider";

    public IProvider GenerateProvider(IList<string> args)
    {
        var typeAsString = args[0];
        var id = int.Parse(args[1]);
        var energyOutput = double.Parse(args[2]);

        var type = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name.Equals(typeAsString + baseClassName));

        var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        
        IProvider provider = (IProvider) ctors[0].Invoke(new object[] {id, energyOutput});
        
        return provider;
    }
}