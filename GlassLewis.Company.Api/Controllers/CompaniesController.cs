using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlassLewis.Company.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IList<CompanyDto>> GetAllCompany()
        {
            return  await _companyService.GetAllCompany();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<CompanyResponse> GetCompanyById(int id)
        {
            return await _companyService.GetCompanyById(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<CompanyResponse> AddCompany([FromBody]CompanyDto companyDto)
        {
            return await _companyService.AddCompany(companyDto);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<CompanyResponse> UpdateCompany([FromBody] CompanyDto companyDto)
        {
            return await _companyService.UpdateCompany(companyDto);
        }
    }
}