using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Microsoft.Extensions.Logging;

namespace Fortytwo.PracticalTest.Application.Posts.Queries;

internal class GetPostByIdQueryHandler : IGetPostByIdQueryHandler
{
    private readonly IPostsReadRepository _postReadRepository;
    private readonly ILogger<GetPostByIdQueryHandler> _logger;

    public GetPostByIdQueryHandler(IPostsReadRepository postReadRepository, ILogger<GetPostByIdQueryHandler> logger)
    {
        _postReadRepository = postReadRepository ?? throw new ArgumentNullException(nameof(postReadRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<GetPostByIdResponse>> HandleAsync(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postReadRepository.GetByIdAsync(query.Id, cancellationToken);
            if (post == null)
            {
                return OperationResult<GetPostByIdResponse>.NotFound();
            }

            var response = new GetPostByIdResponse(post.Id, post.Title, post.Body, post.CreatedByUserId, post.CreatedAt);
            return OperationResult<GetPostByIdResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the post with Id {Id}", query.Id);
            return OperationResult<GetPostByIdResponse>.Failure();
        }
    }
}