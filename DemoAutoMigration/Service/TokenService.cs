using DemoAutoMigration.IRepository;
using DemoAutoMigration.IService;
using DemoAutoMigration.Models;
using DemoAutoMigration.ResponseObject;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DemoAutoMigration.Service
{
    public class TokenService : ITokenService
    {
        private readonly IBaseRepository<User> userRepo;

        public TokenService(IBaseRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ResponseToken generateToken(User user, IConfiguration _configuration)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddSeconds(20),
                signingCredentials: signIn);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            userRepo.updateObject(user);

            return new ResponseToken
            {
                statusCode = System.Net.HttpStatusCode.OK,
                token = accessToken,
                refreshToken = refreshToken,
                message = "OK",
            };
        }

        public string GetEmailFromExpiredToken(string token)
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string user = jwt.Claims.First(c => c.Type == "Email").Value;
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
