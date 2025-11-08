namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record CreatePostCommand(string Title, string Body, int CreatedByUserId);