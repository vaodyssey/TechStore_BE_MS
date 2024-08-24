using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Auth.Models;
using TechStore.Auth.Payload;
using TechStore.Auth.Repositories;
using TechStore.Auth.Services;

namespace TechStore.Auth.Test.AuthService
{
  
    public class RegisterServiceTest
    {
        private IAuthService _service;
        private IUnitOfWork _unitOfWork;
        public RegisterServiceTest(IAuthService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        [Fact]
        public void T0_RegistrationSuccess()
        {
            var request = PrepareRegisterRequest();
            var result = _service.Register(request);
            AssertRegistrationSuccess(result, request);
            CleanupAfterRegistration(request);
        }
        [Fact]
        public void T1_RegistrationDuplicatedEmail()
        {
            var request = PrepareRegisterRequest();            
            _service.Register(request);
            var duplicatedResult = _service.Register(request);
            AssertRegistrationDuplicated(duplicatedResult);
            CleanupAfterRegistration(request);
        }
        private RegisterRequest PrepareRegisterRequest()
        {
            return new RegisterRequest
            {
                Username = "hello",
                Email = "test@mail.com",
                Phone = "0123456789",
                Password = "123456",
                Address = "ABC Main Street"
            };
        }
        private void AssertRegistrationSuccess(ServiceResponse response, RegisterRequest request)
        {
            Assert.Equal(201, response.ResponseCode);
            User testUser = _unitOfWork.UserRepository.Get(user => user.Email == request.Email).FirstOrDefault();
            Assert.NotNull(testUser);
        }
        private void AssertRegistrationDuplicated(ServiceResponse response)
        {
            Assert.Equal(400, response.ResponseCode);
            
            Assert.Equal("This email is taken. Please try again.",response.Message);
        }
        private void CleanupAfterRegistration(RegisterRequest request)
        {
            User user = _unitOfWork.UserRepository.Get(user => user.Email == request.Email).FirstOrDefault();
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
        }
    }
}
