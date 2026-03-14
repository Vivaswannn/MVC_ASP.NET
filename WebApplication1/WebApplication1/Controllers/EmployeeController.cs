using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee {Id=1,Name="Ravi",Department="HR"},
                new Employee {Id=2,Name="John",Department="IT"}
            };


            ViewBag.Company = "ABC Technologies";
            ViewData["Location"] = "Hyderabad";
            TempData["Message"] = "Welcome to Employee Dashboard";
            //----------------------------------------


            ViewData["Message"] = "Welcome to Employee Portal";
            ViewData["Today"] = DateTime.Now.ToShortDateString();

            ViewBag.Name = "Ravi";
            ViewBag.Department = "IT";
            ViewBag.Salary = 50000;
            return View(employees);
        }
        public IActionResult Create()
        {
            TempData["Success"] = "Employee created successfully";
            return RedirectToAction("Action");
        }

        public IActionResult ListCourses()
        {
            ViewBag.Message = "List of Courses";

            List<string> courses = new List<string>();
            {
                courses.Add("C# Basics");
                courses.Add("ASP.NET Core");
                courses.Add("Entity Framework Core");

                ViewBag.Courses = courses;
                return View();
        }
        }
    }
}