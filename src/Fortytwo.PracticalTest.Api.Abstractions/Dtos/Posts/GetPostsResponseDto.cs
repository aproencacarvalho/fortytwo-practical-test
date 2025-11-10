namespace Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;

public record GetPostsResponseDto(IEnumerable<GetPostsResponseItemDto> Items);