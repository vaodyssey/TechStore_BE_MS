using AutoMapper;
using Microsoft.Identity.Client;
using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Utils.JWT;

namespace TechStore.User.Services.Impl.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTUtils _jwtUtils;
        private RegisterService _registerService;
        private LoginService _loginService;
        private ValidateTokenService _validateTokenService;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IJWTUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
            InitializeChildServices();
        }

        public ServiceResponse Login(LoginRequest request)
        {
            return _loginService.Handle(request);

        }

        public ServiceResponse Register(RegisterRequest registerRequest)
        {
            return _registerService.Handle(registerRequest);
        }

        public ServiceResponse ValidateToken(string token)
        {
            return _validateTokenService.Handle(token);
        }

        private void InitializeChildServices()
        {
            _registerService = new RegisterService(_unitOfWork, _mapper);
            _loginService = new LoginService(_unitOfWork, _jwtUtils);
            _validateTokenService = new ValidateTokenService(_jwtUtils);
        }
    }
}
