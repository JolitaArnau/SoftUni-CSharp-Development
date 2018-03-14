using System;
using System.Collections.Generic;
using System.Linq;

public class DraftManager
{
    private List<Harvester> harvesters = new List<Harvester>();
    private List<Provider> providers = new List<Provider>();

    private string currentMode = "Full";

    private double totalStoredEnergy;
    private double totalMinedOre;

    public string RegisterHarvester(List<string> arguments)
    {
        var message = string.Empty;

        if (arguments[0].Equals("Hammer"))
        {
            try
            {
                var hammerHarvester = HarvesterFactory.CreateHarvester(arguments);
                harvesters.Add(hammerHarvester);

                message =
                    $"Successfully registered Hammer Harvester - {hammerHarvester.Id}";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
        }

        else
        {
            try
            {
                var sonicHarvester = HarvesterFactory.CreateHarvester(arguments);
                harvesters.Add(sonicHarvester);

                message =
                    $"Successfully registered Sonic Harvester - {sonicHarvester.Id}";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
        }

        return message;
    }

    public string RegisterProvider(List<string> arguments)
    {
        var message = string.Empty;

        try
        {
            var provider = ProviderFactory.CreateHarvester(arguments);
            providers.Add(provider);

            message =
                $"Successfully registered {provider.GetType().Name.Replace("Provider", string.Empty)} Provider - {provider.Id}";
        }
        catch (Exception exception)
        {
            message = exception.Message;
        }


        return message;
    }

    public string Day()
    {
        double dayEnergy = 0;
        double dayOre = 0;
        double harvestNeededEnergyForDay = 0;

        dayEnergy = providers.Sum(v => v.EnergyOutput);
        totalStoredEnergy += dayEnergy;

        harvestNeededEnergyForDay += harvesters.Sum(h => h.EnergyRequirement);

        if (totalStoredEnergy >= harvestNeededEnergyForDay)
        {
            if (currentMode == "Full")
            {
                dayOre += harvesters.Sum(h => h.OreOutput);
                totalStoredEnergy -= harvestNeededEnergyForDay;
            }
            else if (currentMode == "Half")
            {
                dayOre += harvesters.Sum(h => (h.OreOutput * 50) / 100);
                totalStoredEnergy -= (harvestNeededEnergyForDay * 60) / 100;
            }

            totalMinedOre += dayOre;
        }

        return
            "A day has passed." +
            $"{Environment.NewLine}" +
            $"Energy Provided: {dayEnergy}" +
            $"{Environment.NewLine}Plumbus Ore Mined: {dayOre}";
    }

    public string Mode(List<string> arguments)
    {
        var mode = arguments[0];
        currentMode = mode;

        return $"Successfully changed working mode to {mode} Mode";
    }

    public string Check(List<string> arguments)
    {
        var id = arguments[0];

        var foundEntity = string.Empty;

        var providerExists = providers.Any(p => p.Id.Equals(id));
        var harvesterExists = harvesters.Any(h => h.Id.Equals(id));

        if (providerExists)
        {
            var provider = providers.FirstOrDefault(h => h.Id.Equals(id));

            foundEntity =
                $"{provider.GetType().Name.Replace("Provider", string.Empty)} Provider - {provider.Id}" +
                $"{Environment.NewLine}" +
                $"Energy Output: {provider.EnergyOutput}";
        }

        else if (harvesterExists)
        {
            var harvester = harvesters.FirstOrDefault(h => h.Id.Equals(id));

            foundEntity =
                $"{harvester.GetType().Name.Replace("Harvester", string.Empty)} Harvester - {harvester.Id}" +
                $"{Environment.NewLine}" +
                $"Ore Output: {harvester.OreOutput}" +
                $"{Environment.NewLine}Energy Requirement: {harvester.EnergyRequirement}";
        }

        else
        {
            foundEntity = $"No element found with id - {id}";
        }

        return foundEntity;
    }

    public string ShutDown()
    {
        var report = "System Shutdown" +
                     $"{Environment.NewLine}" +
                     $"Total Energy Stored: {totalStoredEnergy}" +
                     $"{Environment.NewLine}" +
                     $"Total Mined Plumbus Ore: {totalMinedOre}";

        return report;
    }
}