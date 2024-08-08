using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Services.Interface;

namespace GlassLewis.Company.Api.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _compnayRepo;

        public CompanyService(ICompanyRepository compnayRepo)
        {
            _compnayRepo = compnayRepo;
        }

        #region Company 
        public async Task<IList<CompanyDto>> GetAllCompany()
        {
            return await _compnayRepo.GetAllCompany();
        }

        public async Task<CompanyResponse> GetCompanyById(int companyId)
        {
            return await _compnayRepo.GetCompanyById(companyId);
        }

        public async Task<CompanyResponse> AddCompany(CompanyDto companyDto)
        {
            return await _compnayRepo.AddCompany(companyDto);
        }

        public async Task<CompanyResponse> UpdateCompany(CompanyDto companyDto)
        {
            return await _compnayRepo.UpdateCompany(companyDto);
        }

        #endregion
    }
}
