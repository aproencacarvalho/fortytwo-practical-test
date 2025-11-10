namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;

public interface IExternalPostsService
{
    public Task<string?> GetPostTitleByPostIdAsync(int postId);
}
