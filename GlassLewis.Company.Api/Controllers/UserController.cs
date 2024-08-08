using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GlassLewis.Company.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost, Route("login")]
        public async Task<LoginReponse> Login([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.UserName)
                || string.IsNullOrEmpty(login.Password))
            {
                _logger.LogWarning("User login attempt failed", login?.UserName);
                return new LoginReponse()
                {
                    User = null,
                    Message = "Login failed, either user name or password is invalid",
                    Status = LoginStatus.Failed,
                };
            }

            return await _userService.Login(login);
        }
    }
}