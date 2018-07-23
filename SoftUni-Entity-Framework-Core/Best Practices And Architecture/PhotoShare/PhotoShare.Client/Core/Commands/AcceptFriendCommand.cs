﻿namespace PhotoShare.Client.Core.Commands
{
    using System;
    
    using Contracts;
    using Services.Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private IUserSessionService userSessionService;
        private IFriendshipService friendshipService;

        public AcceptFriendCommand(IUserSessionService userSessionService, IFriendshipService friendshipService)
        {
            this.userSessionService = userSessionService;
            this.friendshipService = friendshipService;
        }

        public string Execute(string command, string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }

            if (!userSessionService.IsLoggedIn() || userSessionService.User.Username != data[0])
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var userUsername = data[0];
            var friendUsername = data[1];

            friendshipService.AddFriend(userUsername, friendUsername);

            return $"{userUsername} accepted {friendUsername} as a friend";
        }
    }
}
