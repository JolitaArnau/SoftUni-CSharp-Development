using System;

public abstract class Provider : IProvider
{
    private const int InitialDurability = 1000;
    private const int DurabilityDailyLoss = 100;

    protected Provider(int id, double energyOutput)
    {
        this.ID = id;
        this.EnergyOutput = energyOutput;
        this.Durability = InitialDurability;
    }
    
    public int ID { get; }
    
    public double Durability { get; protected set; }
    
    public double EnergyOutput { get; protected set; }
    
    public double Produce()
    {
        return this.EnergyOutput;
    }

    public void Broke()
    {
        this.Durability -= DurabilityDailyLoss;
        if (this.Durability < 0)
        {
            throw new ArgumentException();
        }
    }

    public void Repair(double val)
    {
        this.Durability += val;
    }
}