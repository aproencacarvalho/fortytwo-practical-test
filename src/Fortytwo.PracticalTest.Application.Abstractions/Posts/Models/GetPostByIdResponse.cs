namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record GetPostByIdResponse(int Id, string Title, string Body, int CreatedByUserId, DateTime CreatedAt);