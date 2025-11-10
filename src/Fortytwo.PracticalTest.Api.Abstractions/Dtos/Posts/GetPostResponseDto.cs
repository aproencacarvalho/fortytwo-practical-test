namespace Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;

public record GetPostResponseDto(int Id, string Title, string Body, int CreatedByUserId, DateTime CreatedAt);
