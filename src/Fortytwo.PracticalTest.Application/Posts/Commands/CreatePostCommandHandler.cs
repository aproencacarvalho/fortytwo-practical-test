using FluentValidation;
using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Fortytwo.PracticalTest.Application.Posts.Commands;

internal class CreatePostCommandHandler : ICreatePostCommandHandler
{
    private readonly IValidator<CreatePostCommand> _validator;
    private readonly IPostsWriteRepository _postWriteRepository;
    private readonly ILogger<CreatePostCommandHandler> _logger;    

    public CreatePostCommandHandler(IValidator<CreatePostCommand> validator, IPostsWriteRepository postWriteRepository, ILogger<CreatePostCommandHandler> logger)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _postWriteRepository = postWriteRepository ?? throw new ArgumentNullException(nameof(postWriteRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<int>> HandleAsync(CreatePostCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return OperationResult<int>.ValidationError("A validation error occurred while creating a new post.",
                                                            errorDetails: validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var post = new PostEntity
            {
                Title = command.Title,
                Body = command.Body,
                CreatedByUserId = command.CreatedByUserId,
                CreatedAt = DateTime.UtcNow
            };
            var postId = await _postWriteRepository.CreateAsync(post, cancellationToken);
            return OperationResult<int>.Success(postId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new post.");
            return OperationResult<int>.Failure();
        }
    }
}