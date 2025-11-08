using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;

public interface IPostsWriteRepository
{
    Task<int> CreateAsync(PostEntityAttributes post, CancellationToken cancellationToken);
}