using System.Text;
using Fortytwo.PracticalTest.Api.Endpoints;
using Fortytwo.PracticalTest.Api.Extensions;
using Fortytwo.PracticalTest.Application.Extensions;
using Fortytwo.PracticalTest.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Fortytwo.PracticalTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices()
                            .AddInfrastructureServices()
                            .AddApiServices()
                            .AddJwtAuthentication(builder.Configuration)
                            .AddAuthorization()
                            .AddEndpointsApiExplorer()
                            .AddSwagger(builder.Configuration);
            
            var app = builder.Build();

            app.MapPostsEndpoints();
            app.MapSecurityEndpoints();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
