public class PressureProvider : Provider
{
    private const int DurabilityDecrementor = 300;
    private const int EnergyOutputMultiplier = 2;

    public PressureProvider(int id, double energyOutput) : base(id, energyOutput)
    {
        this.EnergyOutput *= EnergyOutputMultiplier;
        this.Durability -= DurabilityDecrementor;
    }
}