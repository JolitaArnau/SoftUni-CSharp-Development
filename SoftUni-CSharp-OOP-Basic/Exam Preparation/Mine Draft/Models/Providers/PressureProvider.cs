public class PressureProvider : Provider
{
    public PressureProvider(string id, double energyOutput) : base(id, energyOutput)
    {
        IncreaseEnergyOutputOnInit();
    }

    private void IncreaseEnergyOutputOnInit()
    {
        EnergyOutput += EnergyOutput * 50 / 100;
    }
}