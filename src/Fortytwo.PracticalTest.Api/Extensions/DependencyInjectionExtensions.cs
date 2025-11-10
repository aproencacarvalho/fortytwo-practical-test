using System;
using System.Text;
using Fortytwo.PracticalTest.Api.Abstractions.Services;
using Fortytwo.PracticalTest.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Fortytwo.PracticalTest.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
        => services.AddHttpContextAccessor()
                   .AddSingleton<IAuthenticatedUserService, AuthenticatedUserService>();

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Authentication:Jwt:Issuer"],
                    ValidAudience = configuration["Authentication:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            }).Services;

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        => services.AddSwaggerGen(setup =>
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token based security",
                };

                var securityReq = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                };

                setup.AddSecurityDefinition("Bearer", securityScheme);
                setup.AddSecurityRequirement(securityReq);
            });
}
