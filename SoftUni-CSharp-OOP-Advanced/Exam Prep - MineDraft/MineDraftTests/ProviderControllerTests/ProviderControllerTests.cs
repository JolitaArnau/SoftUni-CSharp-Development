using System.Linq;
using NUnit.Framework;

[TestFixture]
public class ProviderControllerTests
{
    private IProviderController providerController;
    private IEnergyRepository energyRepo;


    [SetUp]
    public void InitializeControllerAndRepo()
    {
        this.energyRepo = new EnergyRepository();
        this.providerController = new ProviderController(this.energyRepo);
    }

    [Test]
    public void RegisterShouldRegisterAProvider()
    {
        var argsInput = "Pressure 40 100".Split().ToList();
        this.providerController.Register(argsInput);

        var countOfProviders = providerController.Entities.Count;

        Assert.AreEqual(1, countOfProviders, "Count of registered providers is not corect!");
    }

    [Test]
    public void ProduceShouldProduceCorrectAmmount()
    {
        var argsInput = "Pressure 40 100".Split().ToList();

        this.providerController.Register(argsInput);
        this.providerController.Produce();

        var actual = this.providerController.TotalEnergyProduced;

        Assert.AreEqual(200, actual, "Total Energy Produced is not corect!");
    }
}