using AutoMapper;
using StoryTeller.DTO;
using StoryTeller.Models;

namespace StoryTeller.Mapping
{
    public class UserAutoMapper : Profile
    {
        public UserAutoMapper()
        {
            CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}