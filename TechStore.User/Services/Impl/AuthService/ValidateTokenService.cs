using Microsoft.IdentityModel.Tokens;
using TechStore.User.Constants;
using TechStore.User.Payload;
using TechStore.User.Utils.JWT;

namespace TechStore.User.Services.Impl.AuthService
{
    public class ValidateTokenService
    {
        private readonly IJWTUtils _jwtUtils;
        public ValidateTokenService(IJWTUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }
        public ServiceResponse Handle(string token)
        {
            try
            {
                bool isTokenValid = _jwtUtils.IsTokenValid(token);
                if (!isTokenValid) return TokenInvalidResult();
                return TokenValidResult();
            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }
        }
        private ServiceResponse TokenInvalidResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.BAD_REQUEST,
                Message = "Token is invalid",
                Data = new { valid = false }
            };
        }
        private ServiceResponse TokenValidResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Token is valid",
                Data = new { valid = true }
            };
        }
        private ServiceResponse InternalServerErrorResult(Exception e)
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                Message = "Something went wrong with the server. See the logs for more details.",
                Data = e.Message!
            };
        }

    }
}
