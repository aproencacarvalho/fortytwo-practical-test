using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Application.Posts.Commands;
using Fortytwo.PracticalTest.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fortytwo.PracticalTest.Application.UnitTests.Posts;

public class CreatePostCommandHandlerTests
{
    private readonly CreatePostCommandHandler _handler;
    private readonly Mock<IValidator<CreatePostCommand>> _validatorMock;
    private readonly Mock<IPostsWriteRepository> _postWriteRepositoryMock;
    private readonly Mock<ILogger<CreatePostCommandHandler>> _loggerMock;

    public CreatePostCommandHandlerTests()
    {
        _validatorMock = new Mock<IValidator<CreatePostCommand>>();
        _postWriteRepositoryMock = new Mock<IPostsWriteRepository>();
        _loggerMock = new Mock<ILogger<CreatePostCommandHandler>>();
        _handler = new CreatePostCommandHandler(_validatorMock.Object, _postWriteRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ValidCommand_CreatesPost()
    {
        // Arrange
        var createdPostId = 1;
        var command = BuildCreatePostCommand();
        _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _postWriteRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<PostEntityAttributes>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdPostId);
        var utcNow = DateTime.UtcNow;
        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(createdPostId);
        _postWriteRepositoryMock.Verify(r =>
                                    r.CreateAsync(It.Is<PostEntityAttributes>(p =>
                                                    p.Title == command.Title &&
                                                    p.Body == command.Body &&
                                                    p.CreatedByUserId == command.CreatedByUserId &&
                                                    p.CreatedAt.Date == utcNow.Date),
                                                It.IsAny<CancellationToken>()),
                                    Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidCommand_ReturnsFailure()
    {
        // Arrange
        const string errorMessage = "Title is invalid.";
        var command = BuildCreatePostCommand();
        _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure("Title", errorMessage)]));

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorDetails.Should().NotBeNull()
                                    .And.Contain(errorMessage);
        _postWriteRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<PostEntityAttributes>(), It.IsAny<CancellationToken>()),
                                    Times.Never);
    }

    private static CreatePostCommand BuildCreatePostCommand()
    {
        var title = "Test Title";
        var body = "Test Body";
        var userId = 10;
        return new CreatePostCommand(title, body, userId);
    }
}
