using System;
using System.Security.Claims;
using Fortytwo.PracticalTest.Api.Abstractions;
using Fortytwo.PracticalTest.Api.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Fortytwo.PracticalTest.Api.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int? GetAuthenticatedUserId()
    {
        var context = _httpContextAccessor.HttpContext;
        var user = context?.User;

        if (user?.Identity is null || user.Identity.IsAuthenticated == false)
            return null;

        var idClaim = user.FindFirst(SecurityConstants.ClaimTypeUserId);
        if (idClaim is null)
            return null;

        return int.TryParse(idClaim.Value, out var id) ? id : null;
    }
}
