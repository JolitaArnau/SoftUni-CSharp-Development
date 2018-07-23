namespace PhotoShare.Services.Contracts
{
    using Models;
    
    public interface IUserService
    {
        User ById(int id);
        
        User ByUsername(string username);

        User ByUserNameAndPassword(string username, string password);
        
        User Register(string username, string password, string repeatPassword, string email);

        User Login(string username, string password);

        string Logout();

        User ModifyUser(string username, string property, string newValue);

        void Delete(string username);
    }
}