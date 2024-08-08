using FluentValidation;
using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Context;
using GlassLewis.Company.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GlassLewis.Company.Api.Validators
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
            services.AddScoped<IValidator<CompanyDto>, CompanyDtoValidator>(); 
            return services;
        }
    }
}
