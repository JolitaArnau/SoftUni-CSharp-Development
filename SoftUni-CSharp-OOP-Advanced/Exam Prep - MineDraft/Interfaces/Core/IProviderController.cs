using System.Collections.Generic;

public interface IProviderController : IController
{
    double TotalEnergyProduced { get; }
    
    IReadOnlyCollection<IEntity> Entities { get; }

    string Repair(double val);
}