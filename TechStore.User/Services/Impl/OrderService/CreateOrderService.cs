using AutoMapper;
using TechStore.User.Constants;
using TechStore.User.DTOs;
using TechStore.User.Models;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils.JWT;

namespace TechStore.User.Services.Impl.OrderService
{
    public class CreateOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTUtils _jWTUtils;
        private readonly IMapper _mapper;
        private Order _order;
        private CreateOrderRequest _request;
        public CreateOrderService(IUnitOfWork unitOfWork, IJWTUtils jWTUtils, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jWTUtils = jWTUtils;
            _mapper = mapper;
        }
        public ServiceResponse Handle(CreateOrderRequest request)
        {
            try
            {
                _request = request;
                if (!IsTokenValid()) return UnauthorizedAccessResult();
                InsertOrder();
                InsertOrderDetails();
                _unitOfWork.Save();
                return CreateOrderSuccessResult();
            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }
        }
        private bool IsTokenValid()
        {
            string token = _request.Token;
            return _jWTUtils.IsTokenValid(token);
        }
        private void InsertOrder()
        {
            Models.User user = GetUserByToken();
            int amount = GetTotalProductsCount();
            _order = new Order
            {
                CreatedAt = DateTime.Now,
                Status = 0,
                User = user,
                Amount = amount
            };
            _unitOfWork.OrderRepository.Insert(_order);            
        }
        private void InsertOrderDetails()
        {
            var orderDetails = _request.Products;
            foreach (var product in orderDetails)
            {
                OrderDetail orderDetail = _mapper.Map<OrderDetail>(product);
                orderDetail.Order = _order;
                orderDetail.ProductId = product.Id;
                _unitOfWork.OrderDetailRepository.Insert(orderDetail);
            }
        }
        private Models.User GetUserByToken()
        {
            string token = _request?.Token;
            int userId = Int32.Parse(_jWTUtils.GetJwtClaimByType("UserId", token));
            return _unitOfWork.UserRepository.Get(user => user.Id == userId).FirstOrDefault();
        }
        private int GetTotalProductsCount()
        {
            int total = 0;  
            List<NewProductDTO> products = _request.Products;
            foreach (var product in products)
            {
                total += product.Quantity;
            }
            return total;
        }
        private ServiceResponse CreateOrderSuccessResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.CREATE_SUCCESS,
                Message = "Successfully created an order for user.",
                Data = null!
            };
        }
        private ServiceResponse UnauthorizedAccessResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                Message = "This user is not authorized to create an order. Please try again.",
                Data = null!
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
