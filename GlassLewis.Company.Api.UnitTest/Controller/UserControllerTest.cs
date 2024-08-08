using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using GlassLewis.Company.Api.Controllers;
using GlassLewis.Company.Api.Services.Interface;
using GlassLewis.Company.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlassLewis.Company.Api.UnitTest.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockUserService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Login_ReturnsFailedResponse_WhenLoginModelIsNull()
        {
            // Act
            var result = await _controller.Login(null);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.User);
            Assert.Equal(LoginStatus.Failed, result.Staus);
            Assert.Equal("Login failed, either user name or password is invalid", result.Message);
        }

        [Fact]
        public async Task Login_ReturnsFailedResponse_WhenUserNameIsEmpty()
        {
            // Arrange
            var loginModel = new LoginModel { UserName = "", Password = "password" };

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.User);
            Assert.Equal(LoginStatus.Failed, result.Staus);
            Assert.Equal("Login failed, either user name or password is invalid", result.Message);
        }

        [Fact]
        public async Task Login_ReturnsFailedResponse_WhenPasswordIsEmpty()
        {
            // Arrange
            var loginModel = new LoginModel { UserName = "r@gmail.com", Password = "" };

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.User);
            Assert.Equal(LoginStatus.Failed, result.Staus);
            Assert.Equal("Login failed, either user name or password is invalid", result.Message);
        }

        [Fact]
        public async Task Login_ReturnsLoginResponse_WhenLoginIsValid()
        {
            // Arrange
            var loginModel = new LoginModel { UserName = "r@gmail.com", Password = "Test12345" };
            var loginResponse = new LoginReponse
            {
                User = new UserDto { UserID = 1, Email = "r@gmail.com" },
                Message = "Login successful",
                Staus = LoginStatus.Success
            };

            _mockUserService.Setup(service => service.Login(loginModel)).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.User);
            Assert.Equal(LoginStatus.Success, result.Staus);
            Assert.Equal("Login successful", result.Message);
            Assert.Equal(1, result.User.UserID);
            Assert.Equal("r@gmail.com", result.User.Email);
        }
    }
}
