namespace PhotoShare.Services.Contracts
{
    using Models;
    
    public interface ITownService
    {
        Town Create(string townName, string countryName);

        Town ByName(string townName);
    }
}