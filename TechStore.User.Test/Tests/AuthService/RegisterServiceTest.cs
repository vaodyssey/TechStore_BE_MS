﻿using Microsoft.AspNetCore.Identity.Data;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Services;


namespace TechStore.User.Test.Tests.AuthService
{
    [TestCaseOrderer(
    ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
    ordererAssemblyName: "TechStore.User.Test")]
    [Collection("AuthServiceTestCollection")]
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
            Cleanup(request);
        }
        [Fact]
        public void T1_RegistrationDuplicatedEmail()
        {
            var request = PrepareRegisterRequest();
            _service.Register(request);
            var duplicatedResult = _service.Register(request);
            AssertRegistrationDuplicated(duplicatedResult);
            Cleanup(request);
        }
        private Payload.RegisterRequest PrepareRegisterRequest()
        {
            return new Payload.RegisterRequest
            {
                Username = "hello",
                Email = "test@mail.com",
                Phone = "0123456789",
                Password = "123456",
                Address = "ABC Main Street"
            };
        }
        private void AssertRegistrationSuccess(ServiceResponse response, Payload.RegisterRequest request)
        {
            Assert.Equal(201, response.ResponseCode);
            Models.User testUser = _unitOfWork.UserRepository.Get(user => user.Email == request.Email).FirstOrDefault();
            Assert.NotNull(testUser);
        }
        private void AssertRegistrationDuplicated(ServiceResponse response)
        {
            Assert.Equal(400, response.ResponseCode);

            Assert.Equal("This email is taken. Please try again.", response.Message);
        }
        private void Cleanup(Payload.RegisterRequest request)
        {
            Models.User user = _unitOfWork.UserRepository.Get(user => user.Email == request.Email).FirstOrDefault();
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
        }
    }
}
