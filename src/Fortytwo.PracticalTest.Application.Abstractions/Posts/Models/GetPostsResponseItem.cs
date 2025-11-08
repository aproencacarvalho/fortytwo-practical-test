namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record GetPostsResponseItem(int Id, string Title, string Body, int CreatedByUserId, DateTime CreatedAt);