using System.Collections.Generic;
using System.Threading.Tasks;
using Auth_Login.Models;

namespace Auth_Login.AuthenticateLoginRepositories
{
    public interface IAuthenticateLogin
    {
        Task<IEnumerable<UserLogin>> getUser();

        Task<UserLogin> AuthenticateUser(string username, string password);
    }
}
