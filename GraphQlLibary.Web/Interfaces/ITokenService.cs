using GraphQlLibary.Domain.Models;

namespace GraphQlLibary.Web.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);

        User GetUser(string token);

        bool IsAdmin(string token);

        bool IsUser(string token);

        int GetId(string token);
    }
}
