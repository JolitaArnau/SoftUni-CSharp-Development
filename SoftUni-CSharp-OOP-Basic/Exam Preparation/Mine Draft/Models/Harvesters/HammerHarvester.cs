public class HammerHarvester : Harvester
{
    public HammerHarvester(string id, double oreOutput, double energyRequirement) : base(id, oreOutput,
        energyRequirement)
    {
        IncreaseOreOutputOnInit();
        IncreaseEnergyRequirementOnInit();
    }

    private void IncreaseOreOutputOnInit()
    {
        OreOutput += OreOutput * 200 / 100;
    }

    private void IncreaseEnergyRequirementOnInit()
    {
        EnergyRequirement *= 2;
    }
}