using Fortytwo.PracticalTest.Api.Endpoints;
using Fortytwo.PracticalTest.Api.Extensions;
using Fortytwo.PracticalTest.Application.Extensions;
using Fortytwo.PracticalTest.Infrastructure.Extensions;

namespace Fortytwo.PracticalTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApiServices();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            app.MapPostsEndpoints();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.Run();
        }
    }
}
