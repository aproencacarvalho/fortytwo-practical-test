using Fortytwo.PracticalTest.Application.Abstractions.Common;

namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

public record GetPostsQuery(string? TitleOrBodySearchTerm, int? UserIdFilter, int PageNumber, int PageSize) 
: PagedQuery(PageNumber, PageSize);