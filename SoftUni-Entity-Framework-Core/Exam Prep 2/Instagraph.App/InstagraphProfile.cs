using System.Linq;
using AutoMapper;
using Instagraph.DataProcessor.Dto.Export;
using Instagraph.DataProcessor.Dto.Import;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<PictureDto, Picture>();

            CreateMap<UserDto, User>();
            
            CreateMap<FollowerDto, UserFollower>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Follower,
                    opt => opt.Ignore());
            
            CreateMap<PostDto, Post>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Picture,
                    opt => opt.Ignore());

            CreateMap<CommentDto, Comment>()
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content))
                .ForAllOtherMembers(opt=> opt.Ignore());
            
            CreateMap<CommentPostDto, Comment>()
                .ForMember(
                    dest=> dest.PostId,
                    opt=> opt.MapFrom(src=> src.Id));

            CreateMap<User, UserExportDto>()
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username))
                .ForMember(
                    dest => dest.MostComments,
                    opt => opt.MapFrom(src => src.Posts.Count == 0 ? 0 : src.Posts.Max(p => p.Comments.Count)));


        }
    }
}
