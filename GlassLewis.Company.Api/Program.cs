using GlassLewis.Company.Api.Services;
using GlassLewis.Company.Api.Repository;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger;
using Microsoft.OpenApi.Models;
using GlassLewis.Company.Api.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "GlassLewis.Company.Api", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var configuration = builder.Configuration;
builder.Services.AddDALServices(configuration);
builder.Services.AddServices(configuration);
builder.Services.AddCors(c =>
    {
        c.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
                var parm = configuration?.GetSection("AllowSpecificOrigins")?.Value?.Split(";");

                builder.WithOrigins(parm)
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .Build();
            });
    });
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();  
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
