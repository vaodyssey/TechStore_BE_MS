using AutoMapper;
using TechStore.User.DTOs;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils.JWT;

namespace TechStore.User.Services.Impl.OrderService
{
    public class OrderService : IOrderService
    {
        private CreateOrderService _createOrderService;
        private IUnitOfWork _unitOfWork;
        private IJWTUtils _jwtUtils;
        private IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork,IJWTUtils jwtUtils,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;   
            _mapper = mapper;   
            InitializeChildServices();
        }
        public ServiceResponse Create(CreateOrderRequest request)
        {
            return _createOrderService.Handle(request);   
        }
        private void InitializeChildServices()
        {
            _createOrderService = new CreateOrderService(_unitOfWork, _jwtUtils, _mapper);
        }
    }
}
