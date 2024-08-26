using AutoMapper;
using TechStore.Auth.Models;
using TechStore.Auth.Payload;

namespace TechStore.Auth.Configs
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
            CreateMap<RegisterRequest, User>();
        }
        private void UserAndUserDTO()
        {
            CreateMap<User, UserDTO>();
        }
        private void UpdateUserRequestAndUser()
        {
            CreateMap<UpdateUserRequest, User>();
        }
    }

}
