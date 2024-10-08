﻿using Newtonsoft.Json;
using System.Dynamic;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Services;

namespace TechStore.User.Test.Tests.AuthService
{
    [TestCaseOrderer(
    ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
    ordererAssemblyName: "TechStore.User.Test")]
    [Collection("AuthServiceTestCollection")]
    public class TokenValidationServiceTest
    {
        private IAuthService _service;
        private IUnitOfWork _unitOfWork;
        public TokenValidationServiceTest(IAuthService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        [Fact]
        public void T0_TokenValid()
        {
            var request = PrepareLoginRequest();
            var loginRes = _service.Login(request);
            var tokenValidRes = _service.ValidateToken((string)loginRes.Data);
            AssertTokenValid(tokenValidRes);
        }
        private void AssertTokenValid(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(objStr);
            bool isValid = expandoObj.valid;
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal(true, isValid);
        }
        private LoginRequest PrepareLoginRequest()
        {
            return new LoginRequest
            {
                Username = "hello",
                Password = "123456"
            };
        }
    }
}
