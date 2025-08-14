using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // TODO: Replace this with real user validation from DB
            if (request.Username == "admin" && request.Password == "123")
            {
                var token = _tokenService.GenerateToken(request.Username, "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
