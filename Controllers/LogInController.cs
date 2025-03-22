using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ChatAppDbContext context;
        private readonly string jwtSecretKey = "YourSuperLongSecretKeyWithMoreThan32Chars!";


        public LogInController(ChatAppDbContext _context)
        {
            context = _context;
        }

        [HttpPost]
        public IActionResult CheckLogIn([FromBody] LoginRequest request)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == request.Username);
            if (user == null)
            {
                return Unauthorized(new LoginResponse { Success = false, Message = "User not found" });
            }

            try
            {
                if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    var token = GenerateJwtToken(user.Id);

                    return Ok(new LoginResponse
                    {
                        Success = true,
                        Message = "Login successful",
                        AccessToken = token
                    });
                }
            }
            catch (BCrypt.Net.SaltParseException)
            {
                if (user.Password == request.Password) // Handle old passwords
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    context.SaveChanges();

                    var token = GenerateJwtToken(user.Id);

                    return Ok(new LoginResponse
                    {
                        Success = true,
                        Message = "Login successful (password updated)",
                        AccessToken = token
                    });
                }
            }

            return Unauthorized(new LoginResponse { Success = false, Message = "Invalid credentials" });
        }

        [HttpPost("register")]
        public IActionResult RegisterNewUser([FromBody] User newUser)
        {

            if (context.Users.FirstOrDefault(u => u.Id == newUser.Id) != null)
            {
                return Unauthorized(new LoginResponse { Success = false, Message = "user already exist!" });
            }
            context.AddUser(newUser);
            var request = new LoginRequest { Username = newUser.Id, Password = newUser.Password };
            var token = GenerateJwtToken(newUser.Id);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                AccessToken = token
            });
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "ChatAppServer",
                audience: "ChatAppClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
    }
}
