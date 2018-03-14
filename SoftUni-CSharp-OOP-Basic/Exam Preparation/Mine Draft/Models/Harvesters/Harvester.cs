using System;

public abstract class Harvester
{
    private const int MaxEnergyRequirement = 20000;
    private const string InvalidRegisterException = " is not registered, because of it's ";

    private double oreOutput;
    private double energyRequirement;

    protected Harvester(string id, double oreOutput, double energyRequirement)
    {
        Id = id;
        OreOutput = oreOutput;
        EnergyRequirement = energyRequirement;
    }

    public string Id { get; }

    public double OreOutput
    {
        get => oreOutput;

        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(Harvester)}{InvalidRegisterException}{nameof(OreOutput)}");
            }

            oreOutput = value;
        }
    }

    public double EnergyRequirement
    {
        get => energyRequirement;

        protected set
        {
            if (value < 0 || value > MaxEnergyRequirement)
            {
                throw new ArgumentException(
                    $"{nameof(Harvester)}{InvalidRegisterException}{nameof(EnergyRequirement)}");
            }

            energyRequirement = value;
        }
    }
}