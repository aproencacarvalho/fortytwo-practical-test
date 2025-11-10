using System;
using Fortytwo.PracticalTest.Api.Abstractions.Dtos.Posts;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

namespace Fortytwo.PracticalTest.Api.Extensions;

public static class ApplicationResponseExtensions
{
    public static GetPostsResponseDto ToDtoAsyncEnumerable(this GetPostsResponse response)
     => new (
        response.Posts.Select(item => new GetPostsResponseItemDto(
            item.Id,
            item.Title,
            item.Body,
            item.CreatedByUserId,
            item.CreatedAt)));

    public static GetPostResponseDto ToDto(this GetPostByIdResponse item)
        => new (
            item.Id,
            item.Title,
            item.Body,
            item.CreatedByUserId,
            item.CreatedAt);
}
