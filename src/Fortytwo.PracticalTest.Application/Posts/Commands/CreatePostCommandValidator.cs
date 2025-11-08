using FluentValidation;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;
using Fortytwo.PracticalTest.Domain;

namespace Fortytwo.PracticalTest.Application.Posts.Commands;

internal class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>, IValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(PostEntityConstants.TitleMaxLength)
            .WithMessage($"Title cannot exceed {PostEntityConstants.TitleMaxLength} characters.");

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage("Body is required.")
            .MaximumLength(PostEntityConstants.BodyMaxLength)
            .WithMessage($"Body cannot exceed {PostEntityConstants.BodyMaxLength} characters.");

        RuleFor(x => x.CreatedByUserId)
            .GreaterThan(0)
            .WithMessage("Created By User Id is invalid.");
    }
} 