namespace SIS.Framework.ActionsResults.Contracts
{
    using Base;
    
    public interface IRedirectable : IActionResult
    {
        string RedirectUrl { get; }
    }
}