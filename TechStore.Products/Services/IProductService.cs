using TechStore.Products.Payload;

namespace TechStore.Products.Services
{
    public interface IProductService
    {
        public ServiceResponse GetBy(GetByRequest request);
        public ServiceResponse GetById(int id);
    }
}
