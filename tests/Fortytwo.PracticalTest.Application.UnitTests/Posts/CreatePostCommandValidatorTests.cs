using FluentAssertions;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Application.Posts.Commands;
using Fortytwo.PracticalTest.Domain;

namespace Fortytwo.PracticalTest.Application.UnitTests.Posts;

public class CreatePostCommandValidatorTests
{
    private readonly CreatePostCommandValidator _validator;
    private const string ValidTitle = "Valid Title";
    private const string ValidBody = "Valid Body";
    private const int ValidUserId = 1;

    public CreatePostCommandValidatorTests()
    {
        _validator = new CreatePostCommandValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_InvalidTitle_ReturnsValidationErrors(string? title)
    {
        // Arrange
        var command = new CreatePostCommand(title, ValidBody, ValidUserId);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreatePostCommand.Title));
    }

    [Fact]
    public void Validate_TitleExceedsMaxLength_ReturnsValidationError()
    {
        // Arrange
        var longTitle = new string('A', PostEntityConstants.TitleMaxLength + 1);
        var command = new CreatePostCommand(longTitle, ValidBody, 1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreatePostCommand.Title));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_InvalidBody_ReturnsValidationErrors(string? body)
    {
        // Arrange
        var command = new CreatePostCommand(ValidTitle, body, ValidUserId);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreatePostCommand.Body));
    }

    [Fact]
    public void Validate_BodyExceedsMaxLength_ReturnsValidationError()
    {
        // Arrange
        var longBody = new string('A', PostEntityConstants.BodyMaxLength + 1);
        var command = new CreatePostCommand(ValidTitle, longBody, 1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreatePostCommand.Body));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidUserId_ReturnsValidationErrors(int createdByUserId)
    {
        // Arrange
        var command = new CreatePostCommand(ValidTitle, ValidBody, createdByUserId);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreatePostCommand.CreatedByUserId));
    }

    [Fact]
    public void Validate_ValidCommand_ReturnsNoValidationErrors()
    {
        // Arrange
        var command = new CreatePostCommand("Valid Title", "Valid Body", 1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
