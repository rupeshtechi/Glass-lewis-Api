using GlassLewis.Company.Api.Repository.Context;
using GlassLewis.Company.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GlassLewis.Company.Api.Repository
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompanyContext>(o => o.UseSqlServer(configuration.GetConnectionString("GlassLewis")));

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
