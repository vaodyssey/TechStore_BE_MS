using AutoMapper;
using TechStore.User.Constants;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils;

namespace TechStore.User.Services.Impl.UserService
{
    public class UpdateUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Models.User _user;
        private UpdateUserRequest _request;
        public UpdateUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ServiceResponse Handle(UpdateUserRequest request)
        {
            try
            {
                _request = request;
                FindUserById();
                if (_user == null) return UserNotFoundResult();
                UpdateUserDetails();
                _unitOfWork.Save();
                return UpdateUserSuccessfulResult();
            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }

        }
        private void FindUserById()
        {
            _user = _unitOfWork.UserRepository.Get(user => user.Id == _request.Id).FirstOrDefault();
        }
        private void UpdateUserDetails()
        {
            _mapper.Map<UpdateUserRequest, Models.User>(_request, _user);
            _user.Password = PasswordUtils.HashPassword(_request.Password);
        }
        private ServiceResponse UpdateUserSuccessfulResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.UPDATE_SUCCESS,
                Message = "Successfully updated the details for the user.",
                Data = null!
            };
        }
        private ServiceResponse UserNotFoundResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.NOT_FOUND,
                Message = "The given userId does not match any user in our database. Please try again.",
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
