using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Services.Interface;
using GlassLewis.Company.Api.Utilities;

namespace GlassLewis.Company.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAthentication _jwtAuth;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepo, IAthentication jwtAuth,
            ILogger<UserService> logger)
        {
            _userRepo = userRepo;
            _jwtAuth = jwtAuth;
            _logger = logger;
        }

        public async Task<LoginReponse> Login(LoginModel login)
        {
            var user = await _userRepo.Login(login);

            if (user != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserID = user.UserID,
                    Email =     user.Email,
                    LastName = user.LastName,
                    FirstName = user.FirstName, 
                    IsActive = user.IsActive,
                };
                var tokenString = await this.GetToken(userDto);
                var loginReponse = new LoginReponse()
                {
                    Token = tokenString,
                    User = userDto,
                    Message = "Login successfull",
                    Status = LoginStatus.Success,
                };
                return loginReponse;
            }
            else
            {
                _logger.LogWarning("User login attempt failed", login.UserName);
                return new LoginReponse()
                {
                    User = null,
                    Message = "Login failed, either user name or password is incorrect", 
                    Status = LoginStatus.Failed,
                }; 
            }
        }

        public async Task<UserDto?> GetUserByID(int id)
        {
            var user = await _userRepo.GetUserByID(id);

            if (user == null)
                return null;

            return new UserDto()
            {
                UserID = user.UserID,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                IsActive = user.IsActive,
            };

        }


        public Task<string> GetToken(UserDto userDTO)
        {
            return Task.Run(() =>
            {
                return GetJwtToken(userDTO);
            });
        }

        public string GetJwtToken(UserDto userDto)
        {
           // type of use rcan be made dynamic based on user roles, for assignment it is done in simple way
           return _jwtAuth.GetToken(userDto.UserID, TypeOfRoles.Admin, userDto.Email);
        }
    }
}
