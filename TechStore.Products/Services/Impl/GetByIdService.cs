using AutoMapper;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TechStore.Products.Constants;
using TechStore.Products.DTOs;
using TechStore.Products.Models;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;

namespace TechStore.Products.Services.Impl
{
    public class GetByIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ProductDto _productDto;
        private Product _product;
        private int _id;

        public GetByIdService(IUnitOfWork unitOfWork,IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ServiceResponse Handle(int id)
        {
            try
            {
                _id = id;
                GetById();
                if (_product == null) return ProductNotFoundResult();
                MapToDto();
                return GetByIdSuccessfulResult();
            }
            catch(Exception e)
            {
                return InternalServerErrorResult(e);
            }            
        }
        private void GetById()
        {
            _product = _unitOfWork.ProductRepository.Get(product=> product.Id==_id).FirstOrDefault();
        }
        private void MapToDto()
        {
            _productDto = _mapper.Map<ProductDto>(_product);
            _productDto.BrandName = _product.Brand.Name;            
        }
        private ServiceResponse ProductNotFoundResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.NOT_FOUND,
                Message = "The given id does not match any product in our database. Please try again.",
                Data = null!
            };
        }
        private ServiceResponse GetByIdSuccessfulResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Successfully retrieved all products with given configuration.",
                Data = _productDto!
            };
        }
        private ServiceResponse InternalServerErrorResult(Exception e)
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                Message = "Something went wrong with the server. See the logs for more details.",
                Data = e.Message!
            };
        }
    }
}
