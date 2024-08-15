using AutoMapper;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;

namespace TechStore.Products.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private GetByService _getByService;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeChildServices();
        }
        public ServiceResponse GetBy(GetByRequest request)
        {                        
            return _getByService.Handle(request);
        }

        public ServiceResponse GetById(int id)
        {
            throw new NotImplementedException();
        }
        private void InitializeChildServices()
        {
            _getByService = new GetByService(_unitOfWork,_mapper);
        }
    }
}
