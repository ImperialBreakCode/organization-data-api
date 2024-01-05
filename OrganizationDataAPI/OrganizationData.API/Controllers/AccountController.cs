using Microsoft.AspNetCore.Mvc;
using OrganizationData.API.Extensions;
using OrganizationData.Application.Abstractions.Services.User;
using OrganizationData.Application.DTO.User;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestDTO loginRequestDTO)
        {
            var result = _userService.Login(loginRequestDTO);
            if (result.Token is null)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequestDTO registerRequestDTO)
        {
            var result = _userService.Register(registerRequestDTO);
            return this.ParseAndReturnMessage(result);
        }

        [HttpDelete("Delete/{username}")]
        public IActionResult Delete(string username)
        {
            var result = _userService.DeleteByUsername(username);
            return this.ParseAndReturnMessage(result);
        }
    }
}
