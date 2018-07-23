using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public ModifyUserCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string command, string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }
            
            if (!userSessionService.IsLoggedIn() || userSessionService.User.Username != data[0])
            {
                throw new InvalidOperationException("Invalid credentials!");
            }


            var username = data[0];
            var property = data[1];
            var newValue = data[2];

            if (!property.Equals("Password") && !property.Equals("BornTown") && !property.Equals("CurrentTown"))
            {
                throw new ArgumentException($"Property {property} not supported!");   
            }

           var user = userService.ModifyUser(username, property, newValue);

            return $"User {user.Username} {property} is {newValue}.";
        }
    }
}
