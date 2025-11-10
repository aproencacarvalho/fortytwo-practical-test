
namespace Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;

public record GetPostsResponseItemDto(int Id, string Title, string Body, int CreatedByUserId, DateTime CreatedAt);