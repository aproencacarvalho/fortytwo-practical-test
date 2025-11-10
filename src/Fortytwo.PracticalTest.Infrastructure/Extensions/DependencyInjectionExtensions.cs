using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Infrastructure.JsonPlaceHolderApi;
using System;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Infrastructure.Common;
using Fortytwo.PracticalTest.Infrastructure.Posts;
using Microsoft.Extensions.DependencyInjection;

namespace Fortytwo.PracticalTest.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddSingleton<PostsRepository>()
                    .AddSingleton<IPostsReadRepository>(provider => provider.GetRequiredService<PostsRepository>())
                    .AddSingleton<IPostsWriteRepository>(provider => provider.GetRequiredService<PostsRepository>())
                    .AddSingleton<IUserService, UserService>()
                    .AddSingleton<IExternalPostsService, ExternalPostsService>()
                    // Typed client for JsonPlaceholder API
                    .AddHttpClient<JsonPlaceHolderApiClient>(client =>
                    {
                        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                        client.Timeout = TimeSpan.FromSeconds(10);
                    }).Services;
}