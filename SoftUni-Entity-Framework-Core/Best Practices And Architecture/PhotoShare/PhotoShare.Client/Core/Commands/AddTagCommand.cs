namespace PhotoShare.Client.Core.Commands
{
    using System;
    
    using Utilities;
    using Contracts;
    using Services.Contracts;
    

    public class AddTagCommand : ICommand
    {
        private IAlbumService albumService;
        private IUserSessionService userSessionService;

        public AddTagCommand(IAlbumService albumService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string command, string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }

            var tagName = data[0].ValidateOrTransform();

            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var tag = this.albumService.CreateTag(tagName);

            return $"Tag {tag.Name} was added successfully!";
        }
    }
}