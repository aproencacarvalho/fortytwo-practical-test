using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Microsoft.Extensions.Logging;

namespace Fortytwo.PracticalTest.Application.Posts.Queries;

internal class GetPostsQueryHandler : IGetPostsQueryHandler
{
    private readonly IPostsReadRepository _postReadRepository;
    private readonly ILogger<GetPostsQueryHandler> _logger;

    public GetPostsQueryHandler(IPostsReadRepository postReadRepository, ILogger<GetPostsQueryHandler> logger)
    {
        _postReadRepository = postReadRepository ?? throw new ArgumentNullException(nameof(postReadRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<GetPostsResponse>> HandleAsync(GetPostsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var posts = await _postReadRepository.GetWithFiltersAsync(query.TitleOrBodySearchTerm, query.UserIdFilter, query.PageNumber, query.PageSize, cancellationToken);
            var items = posts.Select(post => new GetPostsResponseItem(post.Id, post.Title, post.Body, post.CreatedByUserId, post.CreatedAt));
            return OperationResult<GetPostsResponse>.Success(new GetPostsResponse(items));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving posts with filters {TitleOrBodySearchTerm} and {UserIdFilter}.", query.TitleOrBodySearchTerm, query.UserIdFilter);
            return OperationResult<GetPostsResponse>.Failure();
        }
    }
}