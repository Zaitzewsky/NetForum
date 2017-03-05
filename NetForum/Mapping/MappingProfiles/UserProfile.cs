using AutoMapper;
using Domain.Model;
using Viewmodels.UserAccount;

namespace Mapping.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewmodel>();
            CreateMap<UserViewmodel, User>();
        }
    }
}
