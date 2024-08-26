using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Products.Payload;
using TechStore.Products.Repositories;
using TechStore.Products.Services;

namespace TechStore.Products.Test
{
    public class BrandsServiceTest
    {
        private IBrandService _service;        
        public BrandsServiceTest(IBrandService service)
        {
            _service = service;            
        }
        [Fact]
        public void T0_GetBrandsSuccess()
        {            
            var result = _service.GetAll();
            AssertGetBrandsSuccess(result);
        }
        private void AssertGetBrandsSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            List<ExpandoObject> laptops = JsonConvert.DeserializeObject<List<ExpandoObject>>(objStr);
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal(5, laptops.Count());
        }
    }
}
