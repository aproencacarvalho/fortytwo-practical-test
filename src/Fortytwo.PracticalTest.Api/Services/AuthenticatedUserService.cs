using System;
using Fortytwo.PracticalTest.Api.Abstractions.Services;

namespace Fortytwo.PracticalTest.Api.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public int GetAuthenticatedUserId()
    {
        return 1;
    }
}
