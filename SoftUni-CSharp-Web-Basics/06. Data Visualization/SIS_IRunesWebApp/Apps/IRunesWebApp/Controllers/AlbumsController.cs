namespace IRunesWebApp.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Web;
    using Microsoft.EntityFrameworkCore;
    using SIS.WebServer.Results;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using Models;

    public class AlbumsController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse CreatePost(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            var name = request.FormData["name"].ToString();
            var cover = request.FormData["cover"].ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(cover))
            {
                return this.View("Create");
            }

            var user = this.Db.Users.FirstOrDefault(x => x.Username == username);

            user.Albums.Add(new Album {Name = name, Cover = cover});
            this.Db.SaveChanges();

            var response = All(request);
            return response;
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            var albumId = request.QueryData["id"].ToString();

            var album = this.Db.Albums.Include(x => x.Tracks).FirstOrDefault(x => x.Id == albumId);
            var albumCover = HttpUtility.UrlDecode(album.Cover);

            var tracksPrice = album.Tracks.Sum(t => t.Price);
            var tracksPriceAfterDiscount = tracksPrice - (tracksPrice * 13 / 100);

            var albumData = new StringBuilder();

            albumData.Append($"<br/><img src=\"{albumCover}\" width=\"250\" height=\"250\"><br/>");
            albumData.Append($"<p class=\"text-center\"><b>Album Name: {album.Name}</b></p>");
            albumData.Append($"<p class=\"text-center\"><b>Album Price: ${tracksPriceAfterDiscount:f2}</b></p>");

            var tracks = album.Tracks.ToArray();

            var sbTracks = new StringBuilder();

            this.ViewBag["tracks"] = "";

            if (tracks.Length > 0)
            {
                for (int i = 1; i < tracks.Length; i++)
                {
                    var track = tracks[i];
                    sbTracks.Append(
                        $"<b>&bull; {i}.</b> <a href=\"/tracks/details?id={track.Id}&albumId={albumId}\">{track.Name}</a></br>");
                }

                this.ViewBag["tracks"] = sbTracks.ToString();
            }

            this.ViewBag["albumId"] = album.Id;
            this.ViewBag["album"] = albumData.ToString();

            return this.View();
        }

        public IHttpResponse All(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            var username = GetUsername(request);

            var user = this.Db.Users.FirstOrDefault(u => u.Username.Equals(username));

            var albums = user.Albums;

            if (albums.Any())
            {
                var listOfAlbums = string.Empty;

                foreach (var album in albums)
                {
                    var albumHtml = $@"<p><a href=""/albums/details?id={album.Id}"">{album.Name}</a></p>";
                    listOfAlbums += albumHtml;
                }

                this.ViewBag["albumsList"] = listOfAlbums;
            }
            else
            {
                this.ViewBag["albumsList"] = "There are currently no albums.";
            }

            return this.View();
        }
    }
}