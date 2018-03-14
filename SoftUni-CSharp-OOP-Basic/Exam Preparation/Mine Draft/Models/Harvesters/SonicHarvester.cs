public class SonicHarvester : Harvester
{
    public SonicHarvester(string id, double oreOutput, double energyRequirement, int sonicFactor) : base(id, oreOutput,
        energyRequirement)
    {
        SonicFactor = sonicFactor;
        
        DecraseEnergyRequirementOnInit();
    }

    private int SonicFactor { get; }

    private void DecraseEnergyRequirementOnInit()
    {
        EnergyRequirement = EnergyRequirement / SonicFactor;
    }
}