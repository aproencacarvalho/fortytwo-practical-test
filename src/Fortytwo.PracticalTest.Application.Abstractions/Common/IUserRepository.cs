using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Application.Abstractions.Common;

public interface IUserRepository
{
    public User? GetUserByUsername(string username);
}
