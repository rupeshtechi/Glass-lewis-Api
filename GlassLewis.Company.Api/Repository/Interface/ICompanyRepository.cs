using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Entities;

namespace GlassLewis.Company.Api.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<CompanyResponse> AddCompany(CompanyDto company);
        Task<CompanyResponse> UpdateCompany(CompanyDto company);
        Task<CompanyResponse> GetCompanyById(int id);
        Task<IList<CompanyDto>> GetAllCompany();
    }
}
