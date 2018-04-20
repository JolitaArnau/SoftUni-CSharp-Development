using System;
using System.Collections.Generic;
using System.Linq;

public class HarvesterController : IHarvesterController
{
    private const string EnergyMode = "Energy";
    private const string HalfMode = "Half";
    private const string FullMode = "Full";

    private const double EnergyModeFactor = 0.2;
    private const double HalfModeFactor = 0.5;
    private const double FullModeFactor = 1;

    private string currentMode = FullMode;


    private readonly IHarvesterFactory harversterFactory;
    private List<IHarvester> harvesters;
    private readonly IEnergyRepository energyRepository;
    private double oreOutput;


    public HarvesterController(IEnergyRepository energyRepository)
    {
        this.harversterFactory = new HarvesterFactory();
        this.harvesters = new List<IHarvester>();
        this.energyRepository = new EnergyRepository();
        this.oreOutput = 0;
        this.currentMode = FullMode;
    }

    public double OreProduced => this.oreOutput;

    public IReadOnlyCollection<IEntity> Entities => this.harvesters.AsReadOnly();

    public string Register(IList<string> args)
    {
        var harvester = harversterFactory.GenerateHarvester(args);

        if (harvester != null)
        {
            return string.Format(Constants.SuccessfullRegistration, harvester.GetType().Name);
        }

        return null;
    }

    public string Produce()
    {
        var neededEnergy = CalculateEnergyNeeded();

        if (this.energyRepository.EnergyStored < neededEnergy)
        {
            return string.Format(Constants.OreOutputToday, 0);
        }

        this.energyRepository.TakeEnergy(neededEnergy);

        var oreOutput = this.CalculateOre();
        this.oreOutput += oreOutput;

        return string.Format(Constants.OreOutputToday, oreOutput);
    }

    private double CalculateOre()
    {
        var oreOutput = this.harvesters.Sum(o => o.OreOutput);

        switch (this.currentMode)
        {
            case EnergyMode:
                oreOutput *= EnergyModeFactor;
                break;
            case HalfMode:
                oreOutput *= HalfModeFactor;
                break;
            case FullMode:
                oreOutput *= FullModeFactor;
                break;
        }

        return oreOutput;
    }

    private double CalculateEnergyNeeded()
    {
        var energyNeeded = this.harvesters.Sum(e => e.EnergyRequirement);

        switch (this.currentMode)
        {
            case EnergyMode:
                energyNeeded *= EnergyModeFactor;
                break;
            case HalfMode:
                energyNeeded *= HalfModeFactor;
                break;
            case FullMode:
                energyNeeded *= FullModeFactor;
                break;
            default:
                break;
        }

        return energyNeeded;
    }


    public string ChangeMode(string mode)
    {
        this.currentMode = mode;

        var brokenHarvesters = new List<IHarvester>();

        foreach (var harvester in harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception e)
            {
                brokenHarvesters.Add(harvester);
            }
        }

        return string.Format(Constants.ModeChangedMsg, this.currentMode);
    }

    public override string ToString()
    {
        return string.Format(Constants.TotalMinedPlumbus, this.OreProduced);
    }
}