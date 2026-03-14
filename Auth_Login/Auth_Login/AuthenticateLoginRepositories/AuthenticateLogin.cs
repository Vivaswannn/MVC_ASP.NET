using System.Collections.Generic;
using System.Threading.Tasks;
using Auth_Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth_Login.AuthenticateLoginRepositories
{
    public class AuthenticateLogin : IAuthenticateLogin
    {
        private readonly LoginDbcontext _context;

        public AuthenticateLogin(LoginDbcontext context)
        {
            _context = context;
        }

        public async Task<UserLogin> AuthenticateUser(string username, string password)
        {
            var succeeded = await _context.UserLogins.FirstOrDefaultAsync(authUser => authUser.UserName == username && authUser.passCode == password);
            return succeeded;
        }

        public async Task<IEnumerable<UserLogin>> getUser()
        {
            return await _context.UserLogins.ToListAsync();
        }
    }
}
