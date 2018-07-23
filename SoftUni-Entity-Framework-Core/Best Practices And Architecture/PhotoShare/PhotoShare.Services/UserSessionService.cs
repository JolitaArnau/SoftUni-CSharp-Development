namespace PhotoShare.Services
{
    using Models;
    using Contracts;

    public class UserSessionService : IUserSessionService
    {

        public User User { get; set; }

        public bool IsLoggedIn() => this.User != null;
    }
}