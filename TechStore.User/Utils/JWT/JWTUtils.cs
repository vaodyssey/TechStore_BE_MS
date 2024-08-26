using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechStore.User.Repositories;

namespace TechStore.User.Utils.JWT
{
    public class JWTUtils : IJWTUtils
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public JWTUtils(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeConfiguration();
        }
        public string GenerateJSONWebToken(Models.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool IsTokenValid(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string userName = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            string email = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value;
            Models.User user = _unitOfWork.UserRepository.Get(user =>
                user.Email == email &&
                user.Username == userName).FirstOrDefault();

            if (user == null) return false;
            return true;
        }
        private void InitializeConfiguration()
        {

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                       .Build();
        }

    }
}
