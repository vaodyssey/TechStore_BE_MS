using Newtonsoft.Json;
using System.Dynamic;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;
using TechStore.Products.Services;

namespace TechStore.Products.Test
{
    public class GetByIdServiceTest
    {
        private IProductService _service;
        private IUnitOfWork _unitOfWork;
        public GetByIdServiceTest(IProductService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [Fact]
        public void T0_GetByIdSuccess()
        {
            int id = 1;
            var result = _service.GetById(id);
            AssertGetByIdSuccess(result);
        }

        [Fact]
        public void T1_GetByIdNotFound()
        {
            int id = 0;
            var result = _service.GetById(id);
            AssertGetByIdNotFound(result);
        }
        private void AssertGetByIdSuccess(ServiceResponse response)
        {            
            string objStr = JsonConvert.SerializeObject(response.Data);
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(objStr);            
            string productName = expandoObj.Name;
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal("Asus ZenBook 14", productName);
        }
        private void AssertGetByIdNotFound(ServiceResponse response)
        {            
            Assert.Equal(404, response.ResponseCode);            
        }
    }
}