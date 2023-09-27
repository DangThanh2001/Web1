using DemoAutoMigration.Models;
using DemoAutoMigration.ResponseObject;
using System.Security.Claims;

namespace DemoAutoMigration.IService
{
    public interface ITokenService
    {
        public ResponseToken generateToken(User user, IConfiguration configuration);
        public string GenerateRefreshToken();
        string GetEmailFromExpiredToken(string token);
    }
}
