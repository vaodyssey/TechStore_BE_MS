using AutoMapper;
using TechStore.Products.DTOs;
using TechStore.Products.Models;
using TechStore.Products.Payload;

namespace TechStore.Products.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            RegisterRequestAndUser();

        }

        private void RegisterRequestAndUser()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.BrandName,option => option.Ignore());
        }
    }

}
