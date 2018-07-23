namespace PhotoShare.Client.Core.Commands
{
    using System;
    
    using Contracts;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private IAlbumService albumService;
        private IUserSessionService userSessionService;

        public UploadPictureCommand(IAlbumService albumService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string command, string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }

            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumName = data[0];
            var pictureTitle = data[1];
            var picturePath = data[2];

            albumService.AddPicture(albumName, pictureTitle, picturePath);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
