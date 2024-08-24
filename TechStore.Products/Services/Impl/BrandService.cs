using AutoMapper;
using TechStore.Products.Constants;
using TechStore.Products.DTOs;
using TechStore.Products.Models;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;

namespace TechStore.Products.Services.Impl
{
    public class BrandService : IBrandService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private List<Brand> _brands;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResponse GetAll()
        {
            _brands = _unitOfWork.BrandRepository.Get().ToList();
            List<BrandDto> brandDtos = new List<BrandDto>();
            foreach (Brand brand in _brands)
            {
                BrandDto brandDto = _mapper.Map<BrandDto>(brand);
                brandDtos.Add(brandDto);    
            }
            return new ServiceResponse()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Successfully retrieved all brands.",
                Data = brandDtos
            };
        }
    }
}
