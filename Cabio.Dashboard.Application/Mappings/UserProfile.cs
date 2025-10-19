using AutoMapper;
using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Application.Dtos.Users;

namespace Cabio.Dashboard.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Domain → DTO
            CreateMap<User, UserSignupDto>();
            CreateMap<User, UserLoginDto>();

            // DTO → Domain
            CreateMap<UserSignupDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));

            CreateMap<UserLoginDto, User>();
        }
    }
}
