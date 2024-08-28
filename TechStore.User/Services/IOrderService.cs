using TechStore.User.DTOs;
using TechStore.User.Payload;

namespace TechStore.User.Services
{
    public interface IOrderService
    {
        ServiceResponse Create(CreateOrderRequest request);                
    }
}
