using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

using Microsoft.Extensions.Caching.Memory;

namespace Login_Auth.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetCookie()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);

            Response.Cookies.Append("MyCookie", "This is a cookie value", option);
            return Content("Cookie created");
        }

        public IActionResult GetCookie()
        {
            string username = Request.Cookies["MyCookie"];

            return Content("cookie value:" + username);
        }

        public IActionResult DeleteCookie(string username)
        {
            Response.Cookies.Delete("MyCookie");
            return Content("Cookie deleted");
        }

        public IActionResult SaveData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveData(int userid)
        {
            return Content("Data saved for user id:" + userid);
        }

        public IActionResult Index2()
        {
            return RedirectToAction("Details", new { id = 1 });
        }

        public IActionResult Details(int id) {
            return Content("Product id: " + id);
                }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("UserName", "Viv");
            return Content("Session has been set");
        }

        public IActionResult GetSession()
        {
            string username = HttpContext.Session.GetString("UserName");
            return Content($"Session Value: {username}");
        }

        private readonly IMemoryCache _cache;

        public StateManagementController(IMemoryCache _cache)
        {
           this. _cache = _cache;
        }

        public IActionResult CacheDemo()
        {
            _cache.Set("User", "Viv");
            string user= _cache.Get<string>("User");
            return Content(user);
        }
    }
}