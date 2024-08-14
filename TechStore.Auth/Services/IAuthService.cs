using Microsoft.AspNetCore.Identity.Data;
using TechStore.Auth.Payload;


namespace TechStore.Auth.Services
{
    public interface IAuthService
    {
        public ServiceResponse Login(Payload.LoginRequest request);
        public ServiceResponse Register(Payload.RegisterRequest request);
        public ServiceResponse ValidateToken(string token);

    }
}
