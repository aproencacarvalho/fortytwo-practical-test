using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Infrastructure.JsonPlaceHolderApi;

namespace Fortytwo.PracticalTest.Infrastructure.Posts;

public class ExternalPostsService : IExternalPostsService
{
    private readonly JsonPlaceHolderApiClient _apiClient;

    public ExternalPostsService(JsonPlaceHolderApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<string?> GetPostTitleByPostIdAsync(int postId)
    {
        var post = await _apiClient.GetPostById(postId).ConfigureAwait(false);
        return post?.title;
    }
}