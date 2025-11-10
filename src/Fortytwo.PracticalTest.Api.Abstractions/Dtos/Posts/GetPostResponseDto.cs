namespace Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;

public record GetPostResponseDto(int Id, string Title, string Body, string ExternalTitle, int CreatedByUserId, DateTime CreatedAt);