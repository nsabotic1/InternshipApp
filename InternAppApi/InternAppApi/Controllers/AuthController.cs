using InternAppApi.Auth;
using InternAppApi.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternAppApi.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.DoLogin(model);

            if (response.Success == true)
            {
                return Ok(response.Data);
            }
            return Unauthorized();
        }

       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _authService.UserExists(model.Username);
            
            if (userExists == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "error", Message = "Editor Already Exists" });
            }

            var result = await _authService.AddUser(model);

            if (result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Editor creation failed!" });
            }

            return Ok(new Response { Status = "Success", Message = "Editor Created Succesfully" });
        }

    }
}