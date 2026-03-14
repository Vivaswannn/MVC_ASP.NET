using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;

namespace StudentManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly StudentDbContext _context;

        public DepartmentController(StudentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }
    }
}