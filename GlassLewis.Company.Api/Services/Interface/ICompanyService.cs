using GlassLewis.Company.Api.Model;

namespace GlassLewis.Company.Api.Services.Interface
{
    public interface ICompanyService
    {
        Task<CompanyResponse> AddCompany(CompanyDto company);
        Task<CompanyResponse> UpdateCompany(CompanyDto company);
        Task<CompanyResponse> GetCompanyById(int id);
        Task<IList<CompanyDto>> GetAllCompany();
    }
}
