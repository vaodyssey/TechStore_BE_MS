using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TechStore.Auth.Payload;
using TechStore.Auth.Services;
using Xunit.Abstractions;
namespace TechStore.Auth.Test;

[TestCaseOrderer("TechStore.Auth.Test.TestCaseOrderer.AuthServiceTestOrderer", "TechStore.Auth.Test")]
public class AuthServiceTest
{
    
    private IAuthService _service;
    public AuthServiceTest(IAuthService service)
    {
        _service = service;
    }
    [Fact]
    public void T1_LoginSuccess()
    {
        var request = PrepareLoginRequest();
        var result = _service.Login(request);        
        AssertLoginSuccess(result);
    }
    [Fact]
    public void T2_TokenValid()
    {
        var request = PrepareLoginRequest();
        var loginRes = _service.Login(request);        
        var tokenValidRes = _service.ValidateToken((string)loginRes.Data);
        AssertLoginSuccess(loginRes);
        AssertTokenValid(tokenValidRes);
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
    private void AssertTokenValid(ServiceResponse response)
    {
        Assert.Equal(200, response.ResponseCode);
        string objStr = JsonConvert.SerializeObject(response.Data);
        dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(objStr);
        bool isValid = expandoObj.valid;
        Assert.Equal(true, isValid);
    }
}