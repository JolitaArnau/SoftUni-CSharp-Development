namespace SIS.Framework.ActionsResults.Contracts
{
    using Base;

    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}