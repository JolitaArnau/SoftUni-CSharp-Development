namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    
    using Models;
    using Contracts;
    using Data;

    public class AlbumService : IAlbumService
    {
        private PhotoShareContext context;
        private IUserService userService;
        private IUserSessionService userSessionService;

        public AlbumService(PhotoShareContext context, IUserService userService, IUserSessionService userSessionService)
        {
            this.context = context;
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public Tag CreateTag(string tagName)
        {
            var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

            if (tag != null)
            {
                throw new ArgumentException($"Tag {tag.Name} exists!");
            }

            tag = new Tag
            {
                Name = tagName
            };

            this.context.Tags.Add(tag);

            context.SaveChanges();

            return tag;
        }

        public Album CreateAlbum(string username, string albumTitle, string bgColorName, string[] tags)
        {
            var user = userService.ByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            object bgColor;

            if (!Enum.TryParse(typeof(Color), bgColorName, out bgColor))
            {
                throw new ArgumentException($"Color {bgColorName} not found!");
            }

            Color backColor = (Color) bgColor;

            if (!tags.All(t => this.TagByName(t) != null))
            {
                throw new ArgumentException("Invalid tags!");
            }

            var album = this.AlbumByName(albumTitle);

            if (album != null)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var albumTags = tags.Select(t => this.TagByName(t));

            album = new Album
            {
                BackgroundColor = backColor,
                Name = albumTitle
            };

            context.Albums.Add(album);

            albumTags.ToList()
                .ForEach(at => context.AlbumTags.Add(new AlbumTag {Album = album, Tag = at}));

            context.AlbumRoles.Add(new AlbumRole {Album = album, User = user, Role = Role.Owner});

            context.SaveChanges();

            return album;
        }

        public Tag TagByName(string name)
        {
            return context.Tags.SingleOrDefault(t => t.Name == name);
        }

        public Album AlbumByName(string name)
        {
            return context.Albums.SingleOrDefault(a => a.Name == name);
        }

        public void AddTagToAlbum(string albumName, string tagName)
        {
            var tag = this.TagByName(tagName);
            var album = this.AlbumByName(albumName);

            if (tag == null || album == null)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            var albumOwner = context.Users
                .SingleOrDefault(u => u.AlbumRoles.Any(ar => ar.Album == album && ar.Role == Role.Owner));

            if (albumOwner != userSessionService.User)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            context.AlbumTags.Add(new AlbumTag {Album = album, Tag = tag});

            context.SaveChanges();
        }

        public string ShareAlbum(int albumId, string username, string permission)
        {
            Album album = context.Albums.SingleOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            var isAlbumOwner = context.AlbumRoles
                .Any(ar => ar.Album == album && ar.Role == Role.Owner && ar.User == userSessionService.User);

            if (!isAlbumOwner)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var user = userService.ByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            object roleObj;

            if (!Enum.TryParse(typeof(Role), permission, out roleObj))
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            var role = (Role) roleObj;

            context.AlbumRoles.Add(new AlbumRole {Album = album, User = user, Role = role});

            context.SaveChanges();

            return album.Name;
        }

        public void AddPicture(string albumName, string pictureTitle, string pictureFilePath)
        {
            var album = AlbumByName(albumName);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var isAlbumOwner = context.AlbumRoles
                .Any(ar => ar.Album == album && ar.Role == Role.Owner && ar.User == userSessionService.User);

            if (!isAlbumOwner)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            context.Pictures.Add(new Picture
            {
                Album = album,
                Title = pictureTitle,
                Path = pictureFilePath,
                UserProfile = userSessionService.User
            });

            context.SaveChanges();
        }
    }
}