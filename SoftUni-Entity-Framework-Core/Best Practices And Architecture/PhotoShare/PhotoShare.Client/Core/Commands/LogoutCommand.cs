namespace PhotoShare.Client.Core.Commands
{
    using System;
    
    using Contracts;
    using Services.Contracts;
    
    public class LogoutCommand : ICommand
    {
        private IUserSessionService userSessionService;
        private IUserService userService;

        public LogoutCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string command, params string[] data)
        {
            if(data.Length != 0)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }

            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            var loggedOutUsername = this.userService.Logout();

            return $"User {loggedOutUsername} successfully logged out!";
        }
    }
}