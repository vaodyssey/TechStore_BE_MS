using TechStore.Auth.Models;

namespace TechStore.Auth.Utils.JWT
{
    public interface IJWTUtils
    {
        public string GenerateJSONWebToken(User user);
        public bool IsTokenValid(string token);
    }
}
