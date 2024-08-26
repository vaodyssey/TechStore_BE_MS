using TechStore.User.Payload;
using TechStore.User.Services;

namespace TechStore.User.Test.Tests.AuthService;
[TestCaseOrderer(
   ordererTypeName: "TechStore.User.Test.Orderers.TestCaseOrderer",
   ordererAssemblyName: "TechStore.User.Test")]
[Collection("AuthServiceTestCollection")]
public class LoginServiceTest
{
    private IAuthService _service;
    public LoginServiceTest(IAuthService service)
    {
        _service = service;
    }

    [Fact]
    public void T0_LoginSuccess()
    {
        var request = PrepareLoginRequest();
        var result = _service.Login(request);
        AssertLoginSuccess(result);
    }


    private LoginRequest PrepareLoginRequest()
    {
        return new LoginRequest
        {
            Username = "hello",
            Password = "123456"
        };
    }

    private void AssertLoginSuccess(ServiceResponse response)
    {
        Assert.Equal(200, response.ResponseCode);
        Assert.NotNull(response.Data);
    }

}