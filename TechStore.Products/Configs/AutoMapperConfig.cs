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
            ProductAndProductDto();
            BrandAndBrandDto();

        }
        private void ProductAndProductDto()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.BrandName,option => option.Ignore());
        }
        private void BrandAndBrandDto()
        {
            CreateMap<Brand, BrandDto>();
        }

    }

}
