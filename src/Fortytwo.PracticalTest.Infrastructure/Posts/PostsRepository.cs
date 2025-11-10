using System.Collections.Concurrent;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;
using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Infrastructure.Posts;

internal class PostsRepository : IPostsReadRepository, IPostsWriteRepository
{
    private static readonly Dictionary<int, PostEntity> _postsStorage = new();
    private static int _nextId = 1;
    private static readonly object _idLock = new();

    public Task<int> CreateAsync(PostEntityAttributes post, CancellationToken cancellationToken)
    {
        var postId = GetNextId();
        var postEntity = new PostEntity
        {
            Id = postId,
            Title = post.Title,
            Body = post.Body,
            CreatedByUserId = post.CreatedByUserId,
            CreatedAt = post.CreatedAt
        };
        _postsStorage.Add(postId, postEntity);
        return Task.FromResult(postId);
    }

    public Task<PostEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Task.FromResult(_postsStorage.TryGetValue(id, out var post) ? post : null as PostEntity);

    public Task<IEnumerable<PostEntity>> GetWithFiltersAsync(string? titleOrBodySearchTerm, int? userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return Task.FromResult(_postsStorage.Values.Where(post =>
            (string.IsNullOrEmpty(titleOrBodySearchTerm) ||
             post.Title.Contains(titleOrBodySearchTerm, StringComparison.OrdinalIgnoreCase) ||
             post.Body.Contains(titleOrBodySearchTerm, StringComparison.OrdinalIgnoreCase)) &&
            (!userId.HasValue || post.CreatedByUserId == userId.Value))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize));
    }

    private static int GetNextId()
    {
        lock (_idLock)
        {
            return _nextId++;
        }
    }
}
