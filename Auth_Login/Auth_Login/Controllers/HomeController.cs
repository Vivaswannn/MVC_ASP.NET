using Auth_Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Auth_Login.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string message = null, string username = null)
        {
            // Prefer query string values (route values) passed during redirect
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.SuccessMessage = message;
            }
            else if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.UserName = username;
            }
            else if (TempData["UserName"] != null)
            {
                ViewBag.UserName = TempData["UserName"];
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
