using GlassLewis.Company.Api.Common;
using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Services.Interface;
using GlassLewis.Company.Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GlassLewis.Company.Api.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAthentication, Athentication>();

            var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = context?.Principal?.Identity?.Name;

                        if (string.IsNullOrEmpty(userId))
                        {
                            // return unauthorized  
                            context?.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtAppSettingOptions[Constants.JwtIssuer],
                    ValidAudience = jwtAppSettingOptions[Constants.JwtAudience],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions[Constants.JwtKey]))
                };
            });

            return services;
        }
    }
}
