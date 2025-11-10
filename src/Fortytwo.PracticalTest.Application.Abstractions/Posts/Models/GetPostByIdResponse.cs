namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record GetPostByIdResponse(int Id, string Title, string Body, string? ExternalTitle, int CreatedByUserId, DateTime CreatedAt);