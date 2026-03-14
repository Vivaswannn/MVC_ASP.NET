using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Auth_Login.AuthenticateLoginRepositories;

namespace Auth_Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticateLogin _loginUser;

        public LoginController(IAuthenticateLogin loginUser)
        {
            _loginUser = loginUser;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            var user = await _loginUser.AuthenticateUser(username, password);

            if (user != null)
            {
                // Redirect with route values (query string) so the Home page can display the message
                return RedirectToAction("Index", "Home", new { message = "Login successful!", username = user.UserName });
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
    }
}