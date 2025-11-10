using Fortytwo.PracticalTest.Domain.Models;

namespace Fortytwo.PracticalTest.Application.Abstractions.Common;

public interface IUserService
{
    bool AuthenticateUser(string username, string password);
    User? GetUserByUsername(string username);
}
