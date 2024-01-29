using AutoMapper;
using SimpleAccountSystem.Dto.Request;
using SimpleAccountSystem.Mvc.Models;

namespace SimpleAccountSystem.Mvc.Configurations.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserModel, IdentityUserRequestDto>();
        }
    }
}
