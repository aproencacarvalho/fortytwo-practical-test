using Fortytwo.PracticalTest.Api.Extensions;
using Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Api.Abstractions.Services;

namespace Fortytwo.PracticalTest.Api.Endpoints
{
    public static class PostsEndpoints
    {
        public static void MapPostsEndpoints(this WebApplication app)
        {
            app.MapGet("/posts", async (string? TitleOrBodySearchTerm, int? CreatedByUserId, int? PageNumber, int? PageSize, IGetPostsQueryHandler getPostsQueryHandler, CancellationToken token, ILogger<Program> logger) =>
            {
                var result = await getPostsQueryHandler.ExecuteRequestHandler(new GetPostsQuery(TitleOrBodySearchTerm, CreatedByUserId, PageNumber ?? 1, PageSize ?? 10), token, logger);
                return result.ToEndpointResult(queryResult => queryResult.ToDtoAsyncEnumerable());
            });

            app.MapGet("/posts/{id}", async (int id, IGetPostByIdQueryHandler getPostByIdQueryHandler, CancellationToken token, ILogger<Program> logger) =>
            {
                var result = await getPostByIdQueryHandler.ExecuteRequestHandler(new GetPostByIdQuery(id), token, logger);
                return result.ToEndpointResult(queryResult => queryResult.ToDto());
            });

            app.MapPost("/posts", async (CreatePostRequestDto request, ICreatePostCommandHandler createPostCommandHandler, IAuthenticatedUserService authenticatedUserService, CancellationToken token, ILogger<Program> logger) =>
            {
                var result = await createPostCommandHandler.ExecuteRequestHandler(new CreatePostCommand(request.Title, request.Body, authenticatedUserService.GetAuthenticatedUserId()), token, logger);
                return result.ToEndpointResult(commandResult => commandResult);
            });
        }
    }
}