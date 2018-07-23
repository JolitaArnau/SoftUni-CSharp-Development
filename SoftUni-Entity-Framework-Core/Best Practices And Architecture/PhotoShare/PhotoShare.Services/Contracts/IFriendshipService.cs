namespace PhotoShare.Services.Contracts
{
    using System.Collections.Generic;

    public interface IFriendshipService
    {
        void AddFriend(string currentUsername, string friendUsername);

        void AcceptFriend(string currentUsername, string friendUsername);

        List<string> ListFriends(string username);
    }
}