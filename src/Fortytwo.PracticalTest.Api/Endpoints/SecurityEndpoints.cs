using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fortytwo.PracticalTest.Api.Abstractions;
using Fortytwo.PracticalTest.Api.Abstractions.Dtos.Common;
using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Fortytwo.PracticalTest.Api.Endpoints;

public static class SecurityEndpoints
{
    public static WebApplication MapSecurityEndpoints(this WebApplication app)
    {
        app.MapPost("/security/authenticate", [AllowAnonymous] (AuthenticationDto authenticationDto, IUserService userService, IConfiguration configuration) =>
        {

            if (userService.AuthenticateUser(authenticationDto.Username, authenticationDto.Password))
            {
                var user = userService.GetUserByUsername(authenticationDto.Username);
                var issuer = configuration["Authentication:Jwt:Issuer"];
                var audience = configuration["Authentication:Jwt:Audience"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Now its ime to define the jwt token which will be responsible of creating our tokens
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // We get our secret from the appsettings
                var key = Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(SecurityConstants.ClaimTypeUserId, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    Audience = audience,
                    Issuer = issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                var jwtToken = jwtTokenHandler.WriteToken(token);

                return Results.Ok(jwtToken);
            }
            else
            {
                return Results.Unauthorized();
            }
        });

        return app;
    }
}
