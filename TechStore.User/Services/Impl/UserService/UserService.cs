using AutoMapper;
using TechStore.User.Payload;
using TechStore.User.Repositories;

namespace TechStore.User.Services.Impl.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private GetUserByIdService _getUserByIdService;
        private UpdateUserService _updateUserService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeChildServices();
        }
        public ServiceResponse GetUserById(int id)
        {
            return _getUserByIdService.Handle(id);
        }

        public ServiceResponse UpdateUser(UpdateUserRequest request)
        {
            return _updateUserService.Handle(request);
        }

        private void InitializeChildServices()
        {
            _getUserByIdService = new GetUserByIdService(_unitOfWork, _mapper);
            _updateUserService = new UpdateUserService(_unitOfWork, _mapper);
        }
    }
}
