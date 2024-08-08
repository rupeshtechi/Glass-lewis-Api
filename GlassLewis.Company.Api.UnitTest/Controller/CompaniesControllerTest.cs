using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using GlassLewis.Company.Api.Controllers;
using GlassLewis.Company.Api.Services.Interface;
using GlassLewis.Company.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewis.Company.Api.UnitTest.Controller
{
    public class CompaniesControllerTests
    {
        private readonly Mock<ICompanyService> _mockCompanyService;
        private readonly Mock<ILogger<CompaniesController>> _mockLogger;
        private readonly CompaniesController _controller;

        public CompaniesControllerTests()
        {
            _mockCompanyService = new Mock<ICompanyService>();
            _mockLogger = new Mock<ILogger<CompaniesController>>();
            _controller = new CompaniesController(_mockCompanyService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllCompany_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            var mockCompanies = new List<CompanyDto> { new CompanyDto 
                { CompanyID = 1, Name = "Test Company", Exchange ="NASDAQ", Ticker = "AAPL", Isin = "US0378331005" }
            };
            _mockCompanyService.Setup(service => service.GetAllCompany()).ReturnsAsync(mockCompanies);

            // Act
            var result = await _controller.GetAllCompany();

            // Assert
            var returnValue = Assert.IsType<List<CompanyDto>>(result);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsOkResult_WithCompany()
        {
            // Arrange
            var mockCompany = new CompanyResponse {
                Status = CompanyStatus.Success,
                Message = "",
                CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Test Company", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" }
            };
            _mockCompanyService.Setup(service => service.GetCompanyById(1)).ReturnsAsync(mockCompany);

            // Act
            var result = await _controller.GetCompanyById(1);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, returnValue?.CompanyDetails?.CompanyID);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockCompanyResponse = new CompanyResponse
            {
                Status = CompanyStatus.Failed,
                Message = "Company not found",
            };
            _mockCompanyService.Setup(service => service.GetCompanyById(1)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _controller.GetCompanyById(1);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal("Company not found", returnValue?.Message);
            Assert.Equal(CompanyStatus.Failed, returnValue?.Status);
        }

        [Fact]
        public async Task AddCompany_ReturnsCreatedAtAction_WithNewCompany()
        {
            // Arrange
            var newCompanyDto = new CompanyDto { Name = "Test Company apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" };
            var mockCompanyResponse = new CompanyResponse
            {
                Status = CompanyStatus.Success,
                Message = "",
                CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Test Company apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" }
            };  
            _mockCompanyService.Setup(service => service.AddCompany(newCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _controller.AddCompany(newCompanyDto);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, returnValue?.CompanyDetails?.CompanyID);
            Assert.Equal(CompanyStatus.Success, returnValue?.Status);
            Assert.Equal("Test Company apple", returnValue?.CompanyDetails?.Name);
        }

        [Fact]
        public async Task AddCompany_ReturnsBadRequest_WhenIsinIsDuplicate()
        {
            // Arrange
            var mockCompanyResponse = new CompanyResponse
            {
                Status = CompanyStatus.Duplicate,
                Message = "Company isin already exits",
            };
            var duplicateIsinCompanyDto = new CompanyDto {  Name = "Test Company apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" };
            _mockCompanyService.Setup(service => service.AddCompany(duplicateIsinCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _controller.AddCompany(duplicateIsinCompanyDto);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal(CompanyStatus.Duplicate, returnValue?.Status);
            Assert.Equal("Company isin already exits", returnValue?.Message);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsOkResult_WithUpdatedCompany()
        {
            // Arrange
            var updateCompanyDto = new CompanyDto { CompanyID = 1, Name = "Test update apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" };
            var mockCompanyResponse = new CompanyResponse
            {
                Status = CompanyStatus.Success,
                Message = "",
                CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Test update apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" }
            }; 
            _mockCompanyService.Setup(service => service.UpdateCompany(updateCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _controller.UpdateCompany(updateCompanyDto);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, returnValue?.CompanyDetails?.CompanyID);
            Assert.Equal("Test update apple", returnValue?.CompanyDetails?.Name);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockCompanyResponse = new CompanyResponse
            {
                Status = CompanyStatus.Failed,
                Message = "Company not found",
            };
            var updateCompanyDto = new CompanyDto { CompanyID = 2, Name = "Test update apple", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US0378331005" };
            _mockCompanyService.Setup(service => service.UpdateCompany(updateCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _controller.UpdateCompany(updateCompanyDto);

            // Assert
            var returnValue = Assert.IsType<CompanyResponse>(result);
            Assert.Equal("Company not found", returnValue?.Message);
            Assert.Equal(CompanyStatus.Failed, returnValue?.Status);
        }
    }
}
