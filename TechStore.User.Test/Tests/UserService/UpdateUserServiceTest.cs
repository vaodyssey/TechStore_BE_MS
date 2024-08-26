using TechStore.User.Payload;
using TechStore.User.Repositories;
using TechStore.User.Services;
using TechStore.User.Utils;

namespace TechStore.User.Test.Tests.UserService
{
    [TestCaseOrderer(
ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
ordererAssemblyName: "TechStore.User.Test")]
    [Collection("UserServiceTestCollection")]
    public class UpdateUserServiceTest
    {
        private IAuthService _authService;
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;
        public UpdateUserServiceTest(IAuthService authService,
            IUserService userService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        [Fact]
        public void T0_UpdateUserSuccess()
        {
            var registerRequest = PrepareRegisterRequest();
            _authService.Register(registerRequest);
            int userId = FindUserIdByEmail(registerRequest.Email);
            var updateRequest = PrepareUpdateRequest(userId);
            var result = _userService.UpdateUser(updateRequest);
            AssertUpdateSuccess(result, userId);
            Cleanup(userId);
        }
        private RegisterRequest PrepareRegisterRequest()
        {
            return new RegisterRequest
            {
                Username = "hello",
                Email = "test@mail.com",
                Phone = "0123456789",
                Password = "123456",
                Address = "ABC Main Street"
            };
        }
        private int FindUserIdByEmail(string email)
        {
            Models.User user = _unitOfWork.UserRepository.Get(user => user.Email == email).FirstOrDefault();
            return user.Id;
        }
        private UpdateUserRequest PrepareUpdateRequest(int userId)
        {
            return new UpdateUserRequest
            {
                Id = userId,
                Email = "updated@mail.com",
                Phone = "0987654321",
                Password = "UpdatedPassword!",
                Address = "UpdatedAddress"
            };
        }
        private void AssertUpdateSuccess(ServiceResponse response, int userId)
        {
            Models.User user = _unitOfWork.UserRepository.Get(user => user.Id == userId).FirstOrDefault();
            Assert.Equal(201, response.ResponseCode);
            Assert.Equal("updated@mail.com", user.Email);
            Assert.Equal("0987654321", user.Phone);
            Assert.True(PasswordUtils.IsPasswordValid("UpdatedPassword!", user.Password));
            Assert.Equal("UpdatedAddress", user.Address);
        }
        private void AssertRegistrationDuplicated(ServiceResponse response)
        {
            Assert.Equal(400, response.ResponseCode);
            Assert.Equal("This email is taken. Please try again.", response.Message);
        }
        private void Cleanup(int userId)
        {
            Models.User user = _unitOfWork.UserRepository.Get(user => user.Id == userId).FirstOrDefault();
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
        }
    }
}
