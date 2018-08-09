using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Instagraph.Data;
using Instagraph.DataProcessor.Dto.Import;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using ValidationContext = AutoMapper.ValidationContext;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private const string PictureImportSuccess = "Successfully imported Picture {0}.";
        private const string UserImportSuccess = "Successfully imported User {0}.";
        private const string PostImportSuccess = "Successfully imported Post {0}";
        private const string CommentImportSuccess = "Successfully imported Comment {0}.";

        private const string UserFollowerImportSuccess =
            "Successfully imported Follower {0} to User {1}.";

        private const string FailureMessage = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var deserializedPictures = JsonConvert.DeserializeObject<PictureDto[]>(jsonString);

            var validPictures = new List<Picture>();

            var sb = new StringBuilder();

            foreach (var pictureDto in deserializedPictures)
            {
                if (!IsValid(pictureDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var picture = new Picture()
                {
                    Path = pictureDto.Path,
                    Size = pictureDto.Size
                };

                validPictures.Add(picture);
                sb.AppendLine(String.Format(PictureImportSuccess, picture.Path));
            }

            context.Pictures.AddRange(validPictures);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var validUsers = new List<User>();

            var sb = new StringBuilder();

            foreach (var userDto in deserializedUsers)
            {
                var profilePicture = context.Pictures.FirstOrDefault(p => p.Path.Equals(userDto.ProfilePicture));

                if (!IsValid(userDto) || profilePicture == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var user = new User()
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    ProfilePicture = profilePicture
                };

                validUsers.Add(user);
                sb.AppendLine(string.Format(UserImportSuccess, userDto.Username));
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var deserializedFollowers = JsonConvert.DeserializeObject<FollowerDto[]>(jsonString);

            var validFollowers = new List<UserFollower>();

            var sb = new StringBuilder();

            foreach (var followerDto in deserializedFollowers)
            {
                var user = context.Users.FirstOrDefault(u => u.Username.Equals(followerDto.User));
                var userFollower = context.Users.FirstOrDefault(u => u.Username.Equals(followerDto.Follower));

                if (!IsValid(followerDto) || user == null || userFollower == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var alreadyFollowed = validFollowers.Any(f => f.User == user && f.Follower == userFollower);

                if (alreadyFollowed)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var follower = new UserFollower();
                follower.User = user;
                follower.Follower = userFollower;

                validFollowers.Add(follower);
                sb.AppendLine(string.Format(UserFollowerImportSuccess, followerDto.Follower, follower.User));
            }

            context.UsersFollowers.AddRange(validFollowers);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PostDto[]), new XmlRootAttribute("posts"));
            var deserializedPosts =
                (PostDto[]) serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validPosts = new List<Post>();

            foreach (var postDto in deserializedPosts)
            {
                var user = context.Users.FirstOrDefault(u => u.Username == postDto.User);
                var picture = context.Pictures.FirstOrDefault(p => p.Path == postDto.Picture);

                if (!IsValid(postDto) || (user == null || picture == null))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var post = Mapper.Map<Post>(postDto);
                post.User = user;
                post.Picture = picture;

                sb.AppendLine($"Successfully imported Post {postDto.Caption}.");

                validPosts.Add(post);
            }

            context.Posts.AddRange(validPosts);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CommentDto[]), new XmlRootAttribute("comments"));
            var deserializedComments =
                (CommentDto[]) serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validComments = new List<Comment>();

            foreach (var commentDto in deserializedComments)
            {
                var user = context.Users.SingleOrDefault(u => u.Username == commentDto.User);

                if (!IsValid(commentDto) || user == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var isParsed = int.TryParse(commentDto.PostId?.Id, out var parsedId);

                if (!isParsed)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var postId = context.Posts.SingleOrDefault(p => p.Id == parsedId)?.Id;
                
                if (postId == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                var comment = Mapper.Map<Comment>(commentDto);
                comment.User = user;
                comment.PostId = postId.Value;

                validComments.Add(comment);
                
                sb.AppendLine(string.Format(CommentImportSuccess, commentDto.Content));
            }
            
            context.Comments.AddRange(validComments);
            context.SaveChanges();
            
            return sb.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}