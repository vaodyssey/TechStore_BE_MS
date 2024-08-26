using Microsoft.AspNetCore.Identity.Data;
using TechStore.User.Payload;


namespace TechStore.User.Services
{
    public interface IAuthService
    {
        public ServiceResponse Login(Payload.LoginRequest request);
        public ServiceResponse Register(Payload.RegisterRequest request);
        public ServiceResponse ValidateToken(string token);

    }
}
