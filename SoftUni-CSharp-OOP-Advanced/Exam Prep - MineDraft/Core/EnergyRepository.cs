public class EnergyRepository : IEnergyRepository
{
    public double EnergyStored { get; set; }
    
    public bool TakeEnergy(double energyNeeded)
    {
        var hasEnoughEnergy = this.EnergyStored >= energyNeeded;

        if (hasEnoughEnergy)
        {
            this.EnergyStored -= energyNeeded;
            return true;
        }

        return false;
    }

    public void StoreEnergy(double energy)
    {
        this.EnergyStored += energy;
    }
}