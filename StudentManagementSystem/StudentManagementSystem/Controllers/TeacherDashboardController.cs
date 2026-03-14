using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Controllers
{
    public class TeacherDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        { 
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
                return RedirectToAction("Login", "Account");

            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalCourses = _context.Courses.Count();
            ViewBag.TotalDepartments = _context.Departments.Count();
            ViewBag.TeacherName = HttpContext.Session.GetString("UserName");

            return View();
        }
    }
}