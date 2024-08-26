using TechStore.Auth.Payload;

namespace TechStore.Auth.Services
{
    public interface IUserService
    {
        ServiceResponse GetUserById(int id);
        ServiceResponse UpdateUser(UpdateUserRequest request);
    }
}
