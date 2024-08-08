using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;
using GlassLewis.Company.Api.Repository.Entities;
using Microsoft.Extensions.Logging;

namespace GlassLewis.Company.Api.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _dbContext;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(CompanyContext dbContext, ILogger<CompanyRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Company 
        public async Task<IList<CompanyDto>> GetAllCompany()
        {
           var companies =  await _dbContext.Companies.ToListAsync();

           if(companies == null || companies.Count == 0)
                return new List<CompanyDto>();

            return companies.Select(x => new CompanyDto
            {
                CompanyID = x.CompanyID,
                CreatedDate = x.CreatedDate,
                Exchange = x.Exchange,
                IsActive = x.IsActive,
                Isin = x.Isin,
                ModifiedDate = x.ModifiedDate,
                Name = x.Name,
                Ticker = x.Ticker,
                Website = x.Website,
            }).ToList();
        }

        public async Task<CompanyResponse> GetCompanyById(int companyId)
        {
            var company = await _dbContext.Companies
                        .FindAsync(companyId);

            if (company == null)
            {
                return new CompanyResponse
                {
                    Status = CompanyStatus.Failed,
                    Message = "Company not found",
                    CompanyDetails = { },
                };
            }

            return new CompanyResponse
            {
                Status = CompanyStatus.Success,
                Message = "Company found",
                CompanyDetails = new CompanyDto
                {
                    CompanyID = company.CompanyID,
                    CreatedDate = company.CreatedDate,
                    Exchange = company.Exchange,
                    IsActive = company.IsActive,
                    Isin = company.Isin,
                    ModifiedDate = company.ModifiedDate,
                    Name = company.Name,
                    Ticker = company.Ticker,
                    Website = company.Website,
                }
            };

        }

        public async Task<CompanyResponse> AddCompany(CompanyDto companyDto)
        {
            var exists = await _dbContext.Companies
                        .Where(s => s.Isin == companyDto.Isin)
                        .AnyAsync();

            if (exists)
            {
                return new CompanyResponse
                {
                    Status = CompanyStatus.Duplicate,
                    Message = "Company Isin already exist",
                    CompanyDetails = { },
                };
            }

            try
            {
                var company = new Companies() 
                { 
                    Name = companyDto.Name,
                    Website = companyDto.Website,
                    Ticker = companyDto.Ticker,
                    Isin = companyDto.Isin,
                    Exchange = companyDto.Exchange,
                    IsActive = companyDto.IsActive,
                };

                await _dbContext.AddAsync(company);
                await _dbContext.SaveChangesAsync();

                return new CompanyResponse
                {
                    Status = CompanyStatus.Failed,
                    Message = "Successfully saved",
                    CompanyDetails = new CompanyDto
                    {
                        CompanyID = company.CompanyID,
                        CreatedDate = company.CreatedDate,
                        Exchange = company.Exchange,
                        IsActive = company.IsActive,
                        Isin = company.Isin,
                        ModifiedDate = company.ModifiedDate,
                        Name = company.Name,
                        Ticker = company.Ticker,
                        Website = company.Website,
                    }
                };
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new CompanyResponse
                {
                    Status = CompanyStatus.Failed,
                    Message = ex.Message,
                    CompanyDetails = { },
                };
            }
        }

        public async Task<CompanyResponse> UpdateCompany(CompanyDto companyDto)
        {
            var exists = await _dbContext.Companies
                        .Where(s => s.Isin == companyDto.Isin && s.CompanyID != companyDto.CompanyID)
                        .AnyAsync();

            if (exists)
            {
                return new CompanyResponse
                {
                    Status = CompanyStatus.Duplicate,
                    Message = "Company Isin already exist",
                    CompanyDetails = { },
                };
            }

            try
            {
                var company = await _dbContext.Companies
                        .FindAsync(companyDto.CompanyID);
                
                if(company == null)
                {
                    return new CompanyResponse
                    {
                        Status = CompanyStatus.Failed,
                        Message = "Company not found",
                        CompanyDetails = { },
                    };
                }

                company.Name = companyDto.Name;
                company.Website = companyDto.Website;
                company.Ticker = companyDto.Ticker;
                company.Isin = companyDto.Isin;
                company.Exchange = companyDto.Exchange;
                company.IsActive = companyDto.IsActive;
                 
                await _dbContext.SaveChangesAsync();
                return new CompanyResponse
                {
                    Status = CompanyStatus.Duplicate,
                    Message = "Successfully saved",
                    CompanyDetails = new CompanyDto
                    {
                        CompanyID = company.CompanyID,
                        CreatedDate = company.CreatedDate,
                        Exchange = company.Exchange,
                        IsActive = company.IsActive,
                        Isin = company.Isin,
                        ModifiedDate = company.ModifiedDate,
                        Name = company.Name,
                        Ticker = company.Ticker,
                        Website = company.Website,
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new CompanyResponse
                {
                    Status = CompanyStatus.Failed,
                    Message = ex.Message,
                    CompanyDetails = { },
                };
            }
        }
        #endregion

    }
}
