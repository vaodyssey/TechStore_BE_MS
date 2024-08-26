using Newtonsoft.Json;
using System.Dynamic;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Services;

namespace TechStore.User.Test.Tests.UserService
{
    [TestCaseOrderer(
   ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
   ordererAssemblyName: "TechStore.User.Test")]
    [Collection("UserServiceTestCollection")]
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
