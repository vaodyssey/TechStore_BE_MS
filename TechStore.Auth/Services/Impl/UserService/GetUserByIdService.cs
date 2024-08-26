using AutoMapper;
using TechStore.Auth.Constants;
using TechStore.Auth.Models;
using TechStore.Auth.Payload;
using TechStore.Auth.Repositories;

namespace TechStore.Auth.Services.Impl.UserService
{
    public class GetUserByIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private int _id;
        private User _user;
        private UserDTO _userDto;
        public GetUserByIdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ServiceResponse Handle(int id)
        {
            try
            {
                _id = id;
                FindUserById();
                if (_user == null) { return UserNotFoundResult(); }
                MapUserToDto();
                return UserFoundResult();
            }
            catch (Exception e)
            {
                return InternalServerErrorResult(e);
            }
        }
        private void FindUserById()
        {
            _user = _unitOfWork.UserRepository.Get(user => user.Id == _id).FirstOrDefault();
        }
        private void MapUserToDto()
        {
            _userDto = _mapper.Map<UserDTO>(_user);
        }
        private ServiceResponse UserFoundResult()
        {
            return new ServiceResponse
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Message = "Successfully found the user based on the given Id.",
                Data = _userDto!
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
