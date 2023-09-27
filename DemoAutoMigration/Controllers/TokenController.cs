using DemoAutoMigration.IService;
using DemoAutoMigration.Models;
using DemoAutoMigration.ResponseObject;
using DemoAutoMigration.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAutoMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly JobContext _context;
        private readonly ITokenService tokenService;

        public TokenController(IConfiguration config, JobContext context,
            ITokenService tokenService)
        {
            _configuration = config;
            _context = context;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login(User _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    return Ok(tokenService.generateToken(user, _configuration));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private User? GetUser(string email, string password)
        {
            return _context.users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var email = tokenService.GetEmailFromExpiredToken(accessToken); //this is mapped to the Name claim by default
            var user = _context.users.SingleOrDefault(u => u.Email == email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");
            var newAccessToken = tokenService.generateToken(user, _configuration);
            return Ok(new ResponseToken()
            {
                token = newAccessToken.token,
                refreshToken = newAccessToken.refreshToken,
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Claims.First(x => x.Type == "Email").Value;
            var user = _context.users.SingleOrDefault(u => u.Email == username);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
