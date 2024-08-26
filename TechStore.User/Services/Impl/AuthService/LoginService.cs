using TechStore.User.Constants;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils;
using TechStore.User.Utils.JWT;

namespace TechStore.User.Services.Impl.AuthService
{
    public class LoginService
    {
        private LoginRequest _request;
        private IUnitOfWork _unitOfWork;
        private IJWTUtils _jwtUtils;
        private Models.User _user;
        private string _jwtToken;
        public LoginService(IUnitOfWork unitOfWork, IJWTUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }
        public ServiceResponse Handle(LoginRequest request)
        {
            try
            {
                _request = request;
                if (!DoesUsernameExist()) return InvalidCredentialsResult();
                if (!IsPasswordValid()) return InvalidCredentialsResult();
                GenerateCredentials();
                return LoginSuccessfulResult();

            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }
        }
        private bool DoesUsernameExist()
        {
            _user = _unitOfWork.UserRepository.Get(user => user.Username == _request.Username).FirstOrDefault();
            if (_user == null) return false;
            return true;
        }
        private bool IsPasswordValid()
        {
            return PasswordUtils.IsPasswordValid(_request.Password, _user.Password);
        }
        private void GenerateCredentials()
        {
            _jwtToken = _jwtUtils.GenerateJSONWebToken(_user);
        }
        private ServiceResponse LoginSuccessfulResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Successfully logged in.",
                Data = _jwtToken!
            };
        }
        private ServiceResponse InvalidCredentialsResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.BAD_REQUEST,
                Message = "Either the username or the password is invalid. Please try again.",
                Data = null!
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
