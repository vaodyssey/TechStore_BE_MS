using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TechStore.Auth.Models;
using TechStore.Auth.Payload;
using TechStore.Auth.Repositories;
using TechStore.Auth.Services;
using Xunit.Abstractions;
namespace TechStore.Auth.Test.AuthService;

[TestCaseOrderer("TechStore.Auth.Test.TestCaseOrderer.AuthServiceTestOrderer", "TechStore.Auth.Test")]
public class LoginServiceTest
{
    private IAuthService _service;
    private IUnitOfWork _unitOfWork;
    public LoginServiceTest(IAuthService service, IUnitOfWork unitOfWork)
    {
        _service = service;
        _unitOfWork = unitOfWork;
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