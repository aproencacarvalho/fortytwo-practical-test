using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Infrastructure.Posts;
using Microsoft.Extensions.DependencyInjection;

namespace Fortytwo.PracticalTest.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddSingleton<PostsRepository>()
                    .AddSingleton<IPostsReadRepository>(provider => provider.GetRequiredService<PostsRepository>())
                    .AddSingleton<IPostsWriteRepository>(provider => provider.GetRequiredService<PostsRepository>());
}