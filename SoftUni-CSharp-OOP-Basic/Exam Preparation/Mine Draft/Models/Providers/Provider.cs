using System;

public abstract class Provider
{
    private const int MaxEnergyOutput = 10000;
    private const string InvalidRegisterException = " is not registered, because of it's ";

    private double energyOutput;

    protected Provider(string id, double energyOutput)
    {
        Id = id;
        EnergyOutput = energyOutput;
    }

    public string Id { get; }

    public double EnergyOutput
    {
        get => energyOutput;

        protected set
        {
            if (value < 0 || value > MaxEnergyOutput)
            {
                throw new ArgumentException($"{nameof(Provider)}{InvalidRegisterException}{nameof(EnergyOutput)}");
            }

            energyOutput = value;
        }
    }
}