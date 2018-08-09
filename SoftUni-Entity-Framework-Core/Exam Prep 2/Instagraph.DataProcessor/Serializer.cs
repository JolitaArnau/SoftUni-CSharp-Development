using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Instagraph.DataProcessor.Dto.Export;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentedPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .Select(up => new
                {
                    Id = up.Id,
                    Picture = up.Picture.Path,
                    User = up.User.Username
                })
                .OrderBy(u => u.Id)
                .ToArray();

            return JsonConvert.SerializeObject(uncommentedPosts, Formatting.Indented);
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {

          var users = context.Users
              .OrderBy(u => u.Id)
              .Where(u => u.Posts
                  .Any(p => p.Comments
                      .Any(c => u.Followers
                          .Any(f => f.FollowerId == c.UserId))))
              .Select(u => new
              {
                  Username = u.Username,
                  Followers = u.Followers.Count
              })
              .ToArray();
               
            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var sb = new StringBuilder();
            
            var users = context
                .Users
                .ProjectTo<UserExportDto>()
                .OrderByDescending(u => u.MostComments)
                .ThenBy(u => u.Username)
                .ToArray();

            var serializer = new XmlSerializer(typeof(UserExportDto[]), new XmlRootAttribute("users"));

            serializer.Serialize(new StringWriter(sb),
                users,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString();
        }
    }
}