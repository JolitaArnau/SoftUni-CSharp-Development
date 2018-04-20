using System.Collections.Generic;
using System.Text;

public class DayCommand : Command
{
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public DayCommand(IList<string> arguments, IHarvesterController harvesterController,
        IProviderController providerController) : base(arguments)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        var sb = new StringBuilder();

        sb.AppendLine(this.harvesterController.Produce());
        sb.AppendLine(this.providerController.Produce());

        return sb.ToString();
    }
}