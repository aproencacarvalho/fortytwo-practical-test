using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Infrastructure.Common;

internal class UserService : IUserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Name = "Administrator", Email = "admin@example.com" }
    };

    public bool AuthenticateUser(string username, string password)
    {
        return username == "admin" && password == "password";
    }

    public User? GetUserByUsername(string username)
        => _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}