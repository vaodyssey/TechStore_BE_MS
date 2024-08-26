using AutoMapper;
using TechStore.User.Constants;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils;

namespace TechStore.User.Services.Impl.AuthService
{
    public class RegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private RegisterRequest _request;
        private readonly IMapper _mapper;
        public RegisterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ServiceResponse Handle(RegisterRequest request)
        {
            try
            {
                _request = request;
                if (IsEmailTaken()) return EmailTakenResult();
                RegisterNewUser();
                return RegistrationSuccessfulResult();
            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }

        }

        private bool IsEmailTaken()
        {
            Models.User user = _unitOfWork.UserRepository.Get(user => user.Email == _request.Email).FirstOrDefault();
            if (user == null) return false;
            return true;
        }
        private void RegisterNewUser()
        {
            Models.User user = _mapper.Map<Models.User>(_request);
            user.Password = PasswordUtils.HashPassword(_request.Password);
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
        }
        private ServiceResponse RegistrationSuccessfulResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.CREATE_SUCCESS,
                Message = "Successfully created an account.",
                Data = null!
            };
        }
        private ServiceResponse EmailTakenResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.BAD_REQUEST,
                Message = "This email is taken. Please try again.",
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
