using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Auth.Payload;
using TechStore.Auth.Repositories;
using TechStore.Auth.Services;

namespace TechStore.Auth.Test.AuthService
{
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
            Assert.Equal(200, response.ResponseCode);
            string objStr = JsonConvert.SerializeObject(response.Data);
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(objStr);
            bool isValid = expandoObj.valid;
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
