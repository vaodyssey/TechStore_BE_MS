using TechStore.User.Payload;

namespace TechStore.User.Services
{
    public interface IUserService
    {
        ServiceResponse GetUserById(int id);
        ServiceResponse UpdateUser(UpdateUserRequest request);
    }
}
