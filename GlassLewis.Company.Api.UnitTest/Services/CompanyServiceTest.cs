using Xunit;
using Moq;
using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewis.Company.Api.UnitTest.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _companyService = new CompanyService(_mockCompanyRepository.Object);
        }

        [Fact]
        public async Task GetAllCompany_ReturnsListOfCompanies()
        {
            // Arrange
            var mockCompanies = new List<CompanyDto> { new CompanyDto { CompanyID = 1, Name = "Test Company" } };
            _mockCompanyRepository.Setup(repo => repo.GetAllCompany()).ReturnsAsync(mockCompanies);

            // Act
            var result = await _companyService.GetAllCompany();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CompanyDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsCompany_WhenCompanyExists()
        {
            // Arrange
            var mockCompany = new CompanyResponse { CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Test Company" } };
            _mockCompanyRepository.Setup(repo => repo.GetCompanyById(1)).ReturnsAsync(mockCompany);

            // Act
            var result = await _companyService.GetCompanyById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, result?.CompanyDetails?.CompanyID);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNull_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockCompany = new CompanyResponse { CompanyDetails = null };

            _mockCompanyRepository.Setup(repo => repo.GetCompanyById(1)).ReturnsAsync(mockCompany);

            // Act
            var result = await _companyService.GetCompanyById(1);

            // Assert
            Assert.Null(result.CompanyDetails);
        }

        [Fact]
        public async Task AddCompany_ReturnsAddedCompany()
        {
            // Arrange
            var newCompanyDto = new CompanyDto { Name = "New Company" };
            var mockCompanyResponse = new CompanyResponse { CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Test Company" } }; 
            
            _mockCompanyRepository.Setup(repo => repo.AddCompany(newCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _companyService.AddCompany(newCompanyDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, result?.CompanyDetails?.CompanyID);
            Assert.Equal("Test Company", result?.CompanyDetails?.Name);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsUpdatedCompany()
        {
            // Arrange
            var updateCompanyDto = new CompanyDto { CompanyID  = 1, Name = "Updated Company" };
            var mockCompanyResponse = new CompanyResponse { CompanyDetails = new CompanyDto { CompanyID = 1, Name = "Updated Company" } }; ;
            _mockCompanyRepository.Setup(repo => repo.UpdateCompany(updateCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _companyService.UpdateCompany(updateCompanyDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyResponse>(result);
            Assert.Equal(1, result?.CompanyDetails?.CompanyID);
            Assert.Equal("Updated Company", result?.CompanyDetails?.Name);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsNull_WhenCompanyDoesNotExist()
        {
            // Arrange
            var updateCompanyDto = new CompanyDto { CompanyID = 1, Name = "Updated Company" };
            var mockCompanyResponse = new CompanyResponse { Status = CompanyStatus.Failed, CompanyDetails = null }; 

            _mockCompanyRepository.Setup(repo => repo.UpdateCompany(updateCompanyDto)).ReturnsAsync(mockCompanyResponse);

            // Act
            var result = await _companyService.UpdateCompany(updateCompanyDto);

            // Assert
            Assert.Null(result.CompanyDetails);
            Assert.Equal(CompanyStatus.Failed, result?.Status);
        }
    }
}
