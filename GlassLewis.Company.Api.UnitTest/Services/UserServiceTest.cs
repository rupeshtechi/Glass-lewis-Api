using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using GlassLewis.Company.Api.Services;
using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Utilities;
using GlassLewis.Company.Api.Model;
using System.Threading.Tasks;
using GlassLewis.Company.Api.Repository.Entities;

namespace GlassLewis.Company.Api.UnitTest.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IAthentication> _mockJwtAuth;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtAuth = new Mock<IAthentication>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new UserService(_mockUserRepository.Object, _mockJwtAuth.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Login_ReturnsSuccessResponse_WhenCredentialsAreCorrect()
        {
            // Arrange
            var loginModel = new LoginModel { UserName = "test@test.com", Password = "password" };
            var user = new Users { UserID = 1, Email = "test@test.com", Password = "password", LastName = "User",  FirstName = "Test", IsActive = true };
            var loginResponse = new LoginReponse {
                Staus = LoginStatus.Success,
                Message = "login successfull",
                Token = "test_token",
                User = new UserDto { UserID = 1, Email = "test@test.com", LastName = "User", FirstName = "Test", IsActive = true }
            }; 

            _mockUserRepository.Setup(repo => repo.Login(loginModel)).ReturnsAsync(user);
            _mockJwtAuth.Setup(auth => auth.GetToken(1, TypeOfRoles.Admin, "test@test.com")).Returns("test_token");

            // Act
            var result = await _userService.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(LoginStatus.Success, result.Staus);
            Assert.Equal("Login successfull", result.Message);
            Assert.Equal("test_token", result.Token);
            Assert.Equal(1, result.User?.UserID);
        }

        [Fact]
        public async Task Login_ReturnsFailedResponse_WhenCredentialsAreIncorrect()
        {
            // Arrange
            var loginModel = new LoginModel { UserName = "r@gmail.com", Password = "wrongpassword" };
            Users? user = null;

            var loginResponse = new LoginReponse
            {
                Staus = LoginStatus.Failed,
                Message = "login failed",
                Token = "",
                User = null
            };
            _mockUserRepository.Setup(repo => repo.Login(loginModel)).ReturnsAsync(user);

            // Act
            var result = await _userService.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(LoginStatus.Failed, result.Staus);
            Assert.Equal("Login failed, either user name or password is incorrect", result.Message);
            Assert.Null(result.User);
        }

        [Fact]
        public async Task GetUserByID_ReturnsUserDto_WhenUserExists()
        {
            // Arrange
            var user = new Users { UserID = 1, Email = "test@test.com", LastName = "User", FirstName = "Test", IsActive = true };
            _mockUserRepository.Setup(repo => repo.GetUserByID(1)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByID_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            Users? user = null;
            _mockUserRepository.Setup(repo => repo.GetUserByID(1)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByID(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetToken_ReturnsTokenString()
        {
            // Arrange
            var userDto = new UserDto { UserID = 1, Email = "test@test.com", LastName = "User", FirstName = "Test", IsActive = true };
            _mockJwtAuth.Setup(auth => auth.GetToken(userDto.UserID, TypeOfRoles.Admin, userDto.Email)).Returns("test_token");

            // Act
            var result = await _userService.GetToken(userDto);

            // Assert
            Assert.Equal("test_token", result);
        }
    }
}
