namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record GetPostsResponse(IEnumerable<GetPostsResponseItem> Posts);
