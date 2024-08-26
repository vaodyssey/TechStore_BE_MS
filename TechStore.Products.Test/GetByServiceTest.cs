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
    public class GetByServiceTest
    {
        private IProductService _service;
        private IUnitOfWork _unitOfWork;
        public GetByServiceTest(IProductService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [Fact]
        public void T0_GetBySearchTerm()
        {
            var request = PrepareSearchTermRequest();
            var result = _service.GetBy(request);
            AssertGetBySearchTermSuccess(result);
        }
        [Fact]
        public void T1_GetByLabel()
        {
            var request = PrepareLabelRequest();
            var result = _service.GetBy(request);
            AssertGetByLabelSuccess(result);
        }
        [Fact]
        public void T2_GetByPriceRange()
        {
            var request = PreparePriceRangeRequest();
            var result = _service.GetBy(request);
            AssertGetByPriceRangeSuccess(result);
        }
        [Fact]
        public void T3_GetByPriceRangeWithPagination()
        {
            var request = PreparePriceRangeWithPaginationRequest();
            var result = _service.GetBy(request);
            AssertGetByPriceRangeWithPaginationSuccess(result);
        }
        private GetByRequest PrepareSearchTermRequest()
        {
            return new GetByRequest
            {
                SearchTerm = "vivobook"
            };
        }
        private GetByRequest PrepareLabelRequest()
        {
            return new GetByRequest
            {
                Label = "HP"
            };
        }
        private GetByRequest PreparePriceRangeRequest()
        {
            return new GetByRequest
            {
                MinPrice = 45000000,
                MaxPrice = 50000000
            };
        }
        private GetByRequest PreparePriceRangeWithPaginationRequest()
        {
            return new GetByRequest
            {
                MinPrice = 40000000,
                MaxPrice = 50000000,
                PageNumber = 1,
                PageSize = 2,
            };
        }
        private void AssertGetBySearchTermSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            dynamic expandoObj = JsonConvert.DeserializeObject<List<ExpandoObject>>(objStr);
            string productName = expandoObj[0].Name;
            Assert.Equal(200, response.ResponseCode);
            Assert.Contains("vivobook", productName.ToLower());

        }
        private void AssertGetByLabelSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            List<ExpandoObject> laptops = JsonConvert.DeserializeObject<List<ExpandoObject>>(objStr);
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal(3, laptops.Count());
        }
        private void AssertGetByPriceRangeSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            List<ExpandoObject> laptops = JsonConvert.DeserializeObject<List<ExpandoObject>>(objStr);
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal(1, laptops.Count());
        }
        private void AssertGetByPriceRangeWithPaginationSuccess(ServiceResponse response)
        {
            string objStr = JsonConvert.SerializeObject(response.Data);
            List<ExpandoObject> laptops = JsonConvert.DeserializeObject<List<ExpandoObject>>(objStr);
            Assert.Equal(200, response.ResponseCode);
            Assert.Equal(2, laptops.Count());
        }
    }
}
