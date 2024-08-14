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
            
        }

        private void RegisterRequestAndUser()
        {
            CreateMap<RegisterRequest, User>();
        }
    }

}
