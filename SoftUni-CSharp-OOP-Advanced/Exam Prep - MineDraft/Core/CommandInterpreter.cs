using System;
using System.Collections.Generic;
using System.Linq;

public class CommandInterpreter : ICommandInterpreter
{
    private const string harvestercontroller = "harvesterController";
    private const string providercontroller = "providerController";

    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController)
    {
        this.HarvesterController = harvesterController;
        this.ProviderController = providerController;
    }

    public IHarvesterController HarvesterController { get; }

    public IProviderController ProviderController { get; }

    public string ProcessCommand(IList<string> args)
    {
        var commandAsString = args[0];
        var commandType = Type.GetType(commandAsString + Constants.CommandSuffix);
        
        var paramsInfo = commandType.GetConstructors().First().GetParameters();

        var ctorParams = new object[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++)
        {
            if (paramsInfo[i].Name == harvestercontroller)
            {
                ctorParams[i] = this.HarvesterController;
            }
            else
            {
                if (paramsInfo[i].Name == providercontroller)
                {
                    ctorParams[i] = this.ProviderController;
                }
                else
                {
                    ctorParams[i] = args.Skip(1).ToList();
                }
            }
        }

        var command = (ICommand) Activator.CreateInstance(commandType, ctorParams);

        return command.Execute();
    }
}