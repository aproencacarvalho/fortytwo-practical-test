using System;
using Fortytwo.PracticalTest.Api.Abstractions.Services;
using Fortytwo.PracticalTest.Api.Services;

namespace Fortytwo.PracticalTest.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
        => services.AddSingleton<IAuthenticatedUserService, AuthenticatedUserService>();
}
