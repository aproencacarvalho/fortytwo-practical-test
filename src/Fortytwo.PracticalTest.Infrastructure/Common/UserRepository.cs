using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Infrastructure.Common;

public class UserRepository : IUserRepository
{
    public User? GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }
}
