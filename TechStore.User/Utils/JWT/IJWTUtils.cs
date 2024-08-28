using TechStore.User.Models;

namespace TechStore.User.Utils.JWT
{
    public interface IJWTUtils
    {
        public string GenerateJSONWebToken(Models.User user);
        public bool IsTokenValid(string token);
        public string GetJwtClaimByType(string type, string token);
    }
}
