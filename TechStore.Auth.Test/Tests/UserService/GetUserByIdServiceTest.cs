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

namespace TechStore.Auth.Test.Tests.UserService
{
    [Collection("Xzy Test Collection")]
    public class GetUserByIdServiceTest
    {
        private IUserService _service;
        private IUnitOfWork _unitOfWork;
        public GetUserByIdServiceTest(IUserService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        [Fact]
        public void T0_GetByIdSuccess()
        {
            int id = 4;
            var result = _service.GetUserById(id);
            AssertGetByIdSuccess(result);
        }
        [Fact]
        public void T1_GetByIdNotFound()
        {
            int id = 0;
            var result = _service.GetUserById(id);
            AssertGetByIdNotFound(result);
        }
        private void AssertGetByIdSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(objStr);
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal("hello", expandoObj.Username);
            Assert.Equal("abc@gmail.com", expandoObj.Email);
        }
        private void AssertGetByIdNotFound(ServiceResponse response)
        {
            Assert.Equal(404, response.ResponseCode);
        }

    }
}
