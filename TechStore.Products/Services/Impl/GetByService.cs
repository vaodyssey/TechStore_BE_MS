using AutoMapper;
using TechStore.Products.Constants;
using TechStore.Products.DTOs;
using TechStore.Products.Models;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;


namespace TechStore.Products.Services.Impl
{
    public class GetByService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private GetByRequest _request;
        private List<ProductDto> _productDtos;
        private List<Product> _products;
        public GetByService(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }   
        public ServiceResponse Handle(GetByRequest request)
        {
            try
            {
                _request = request;
                ResetPreviousSearchResult();
                HandleSearch();
                PaginateResult();
                MapResultToDto();                
                return GetBySuccessfulResult();
            }catch(Exception e)
            {
                return InternalServerErrorResult(e);    
            }

        }
        private void ResetPreviousSearchResult()
        {
            _productDtos = new List<ProductDto> ();            
        }
        private void HandleSearch()
        {
            if (_request.SearchTerm!=null) { SearchBySearchTerm(); }
            if (_request.Label != null) { FilterByBrand(); }
            if (_request.MinPrice > 0 && _request.MaxPrice > 0) { FilterByPriceRange(); }            
            if (_request.SortBy!=null && _request.SortOrder!=null ) { SortResultBy(); }            
        }
        private void MapResultToDto()
        {
            foreach (var item in _products)
            {
                ProductDto dto = _mapper.Map<ProductDto>(item);
                dto.BrandName = item.Brand.Name;
                _productDtos.Add(dto);
            }
        }
        private void SearchBySearchTerm()
        {
            _products = _unitOfWork.ProductRepository.Get(product => product.Name.Contains(_request.SearchTerm)).ToList();
        }
        private void FilterByBrand()
        {
            if (_products == null) //runs when previous conditions are not met (SearchTerm, MinMaxPrice).
                _products = _unitOfWork.ProductRepository.Get().ToList();
            _products = _products.Where(product => product.Brand.Name == _request.Label).ToList();
        }
        private void FilterByPriceRange()
        {
            if (_products==null) //runs when previous conditions are not met (SearchTerm, MinMaxPrice).
                _products = _unitOfWork.ProductRepository.Get().ToList();
            _products = _products.Where(product =>
            (product.Price >= _request.MinPrice)
            && (product.Price <= _request.MaxPrice)).ToList();            
        }
      
        private void SortResultBy()
        {
            if (_products==null) //runs when previous conditions are not met (SearchTerm, MinMaxPrice).
                _products = _unitOfWork.ProductRepository.Get().ToList();
            if (_request.SortBy == "price" && _request.SortOrder == "asc")
            {
                _products = _products.OrderBy(product => product.Price).ToList();
            }
            else if (_request.SortBy == "name" && _request.SortOrder == "asc")
            {
                _products = _products.OrderBy(product => product.Name).ToList();
            }
            else if (_request.SortBy == "price" && _request.SortOrder == "desc")
            {
                _products = _products.OrderByDescending(product => product.Price).ToList();
            }
            else if (_request.SortBy == "name" && _request.SortOrder == "desc")
            {
                _products = _products.OrderByDescending(product => product.Name).ToList();
            }

        }
        private void PaginateResult()        
        {
            if (_products==null) //runs when previous conditions are not met (SearchTerm, MinMaxPrice).
                _products = _unitOfWork.ProductRepository.Get().ToList();
            int actualPageNumber = _request.PageNumber - 1;
            _products = _products.Skip(actualPageNumber*_request.PageSize).Take(_request.PageSize).ToList(); 
        }
        private ServiceResponse GetBySuccessfulResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Successfully retrieved all products with given configuration.",
                Data = _productDtos!
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
