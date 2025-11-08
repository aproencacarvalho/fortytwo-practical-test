using FluentValidation;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Application.Posts.Commands;
using Fortytwo.PracticalTest.Application.Posts.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Fortytwo.PracticalTest.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddPostServices();

    private static IServiceCollection AddPostServices(this IServiceCollection services)
        => services
            .AddSingleton<IValidator<CreatePostCommand>, CreatePostCommandValidator>()
            .AddSingleton<ICreatePostCommandHandler, CreatePostCommandHandler>()
            .AddSingleton<IGetPostByIdQueryHandler, GetPostByIdQueryHandler>()
            .AddSingleton<IGetPostsQueryHandler, GetPostsQueryHandler>();
}
