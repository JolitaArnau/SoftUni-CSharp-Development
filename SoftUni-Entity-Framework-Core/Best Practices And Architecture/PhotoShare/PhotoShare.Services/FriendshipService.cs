namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    using Contracts;
    using Models;
    using Data;

    public class FriendshipService : IFriendshipService
    {
        private readonly PhotoShareContext context;
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;
        

        public FriendshipService(PhotoShareContext context, IUserService userService,
            IUserSessionService userSessionService)
        {
            this.context = context;
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public void AddFriend(string currentUsername, string friendUsername)
        {
            var user = userService.ByUsername(currentUsername);
            
            if(user == null)
            {
                throw new ArgumentException($"{currentUsername} not found!");
            }

            var friend = userService.ByUsername(friendUsername);

            if (friend == null)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            var invitationSent = context.Friendships.Any(fr => fr.User == user && fr.Friend == friend);

            if (invitationSent)
            {
                throw new InvalidOperationException($"{user.Username} has already added {friend.Username} as a friend");
            }

            var friendshipExists = context.Friendships.Any(fr => fr.User == user && fr.Friend == friend) &&
                                   context.Friendships.Any(fr => fr.User == friend && fr.Friend == user);

            if (friendshipExists)
            {
                throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}");
            }

            context.Friendships.Add(new Friendship { User = user, Friend = friend });

            context.SaveChanges();
        }

        public void AcceptFriend(string currentUsername, string friendUsername)
        {
            User user = userService.ByUsername(currentUsername);

            if (user == null)
            {
                throw new ArgumentException($"{currentUsername} not found!");
            }

            User friend = userService.ByUsername(friendUsername);

            if (friend == null)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            var invitationSent = context.Friendships.Any(fr => fr.User == friend && fr.Friend == user);

            if (!invitationSent)
            {
                throw new InvalidOperationException($"{friend.Username} has not added {user.Username} as a friend");
            }

            var friendshipExists = context.Friendships.Any(fr => fr.User == user && fr.Friend == friend) &&
                                   context.Friendships.Any(fr => fr.User == friend && fr.Friend == user);

            if (friendshipExists)
            {
                throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}");
            }

            context.Friendships.Add(new Friendship { User = user, Friend = friend });

            context.SaveChanges();
        }

        public List<string> ListFriends(string username)
        {
            var user = userService.ByUsername(username);

            if(user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var addedFriends = context.Friendships
                .Where(f => f.User == user)
                .Select(f => f.Friend.Username)
                .ToList();

            var acceptedFriends = context.Friendships
                .Where(f => f.Friend == user)
                .Select(f => f.User.Username)
                .ToList();

            return addedFriends.Intersect(acceptedFriends).ToList();
        }
    }
}