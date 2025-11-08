using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;

public interface IPostsReadRepository
{
    Task<PostEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<PostEntity>> GetWithFiltersAsync(string? titleOrBodySearchTerm, int? userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
}