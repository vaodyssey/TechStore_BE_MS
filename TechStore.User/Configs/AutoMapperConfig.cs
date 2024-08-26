using AutoMapper;
using TechStore.User.DTOs;
using TechStore.User.Payload;

namespace TechStore.User.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            RegisterRequestAndUser();
            UserAndUserDTO();
            UpdateUserRequestAndUser();
        }

        private void RegisterRequestAndUser()
        {
            CreateMap<RegisterRequest, Models.User>();
        }
        private void UserAndUserDTO()
        {
            CreateMap<Models.User, UserDTO>();
        }
        private void UpdateUserRequestAndUser()
        {
            CreateMap<UpdateUserRequest, Models.User>();
        }
    }

}
