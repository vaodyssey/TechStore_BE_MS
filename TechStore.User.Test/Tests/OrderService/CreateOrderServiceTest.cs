using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.User.DTOs;
using TechStore.User.Models;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Services;
using Xunit.Abstractions;

namespace TechStore.User.Test.Tests.OrderService
{
    [TestCaseOrderer(
   ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
   ordererAssemblyName: "TechStore.User.Test")]
    [Collection("OrderServiceTestCollection")]
    public class CreateOrderServiceTest
    {
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderServiceTest(IAuthService authService, IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }
        [Fact]
        public void T0_OrderSuccess()
        {
            var token = PrepareToken();
            var request = PrepareOrderRequest(token);
            var result = _orderService.Create(request);
            var order = GetCreatedOrder();
            AssertOrderSuccess(result, order);
            Cleanup(order);
        }

        private string PrepareToken()
        {
            var request = PrepareLoginRequest();
            var result = _authService.Login(request);
            return (string)result.Data;
        }
        private LoginRequest PrepareLoginRequest()
        {
            return new LoginRequest
            {
                Username = "hello",
                Password = "123456"
            };
        }
        private CreateOrderRequest PrepareOrderRequest(string token)
        {
            List<NewProductDTO> products = new List<NewProductDTO>
            {
                new NewProductDTO{Id=1,Quantity=1},
                new NewProductDTO{Id=2,Quantity=3},
                new NewProductDTO{Id=3,Quantity=4},
            };
            return new CreateOrderRequest
            {
                Products = products,
                Token = token
            };
        }
        private Order GetCreatedOrder()
        {
            return _unitOfWork.OrderRepository.Get().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        }
        private void AssertOrderSuccess(ServiceResponse response, Order order)
        {
            List<OrderDetail> orderDetails = order.OrderDetails.ToList();
            Assert.Equal(201, response.ResponseCode);
            Assert.Equal(3, orderDetails.Count());
            Assert.Equal(1, orderDetails[0].ProductId);
            Assert.Equal(2, orderDetails[1].ProductId);
            Assert.Equal(3, orderDetails[2].ProductId);
        }
        private void Cleanup(Order order)
        {
            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.Save();
        }
    }

}
