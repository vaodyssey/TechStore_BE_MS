using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechStore.Auth.Models;
using TechStore.Auth.Repositories;

namespace TechStore.Auth.Utils.JWT
{
    public class JWTUtils : IJWTUtils
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public JWTUtils(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public string GenerateJSONWebToken(User user)
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
            User user = _unitOfWork.UserRepository.Get(user =>
                (user.Email == email) &&
                (user.Username == userName)).FirstOrDefault();

            if (user == null) return false;
            return true;
        }
    }
}
