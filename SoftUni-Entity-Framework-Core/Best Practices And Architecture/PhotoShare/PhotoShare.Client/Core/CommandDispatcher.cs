﻿namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    
    using Contracts;

    public class CommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes.SingleOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var command = InjectServices(commandType);
            
            return command;
        }

        private ICommand InjectServices(Type type)
        {
            var constructor = type.GetConstructors().First();

            var constructorParams = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();
            
            var services = constructorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand) constructor.Invoke(services);

            return command;
        }
    }
}
