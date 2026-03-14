using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail");

            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Email == email);

            if (student == null)
                return View("NoProfile");

            var profile = new StudentProfileViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department.DepartmentName,
                CourseName = student.Course.CourseName,
                CourseDuration = student.Course.Duration,
                CourseFees = student.Course.Fees
            };

            return View(profile);
        }

        public IActionResult UpdateProfile()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail");

            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Email == email);

            if (student == null) return NotFound();

            var profile = new StudentProfileViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department.DepartmentName,
                CourseName = student.Course.CourseName,
                CourseDuration = student.Course.Duration,
                CourseFees = student.Course.Fees
            };

            return View(profile);
        }

        [HttpPost]
        public IActionResult UpdateProfile(StudentProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.Find(model.StudentId);

                if (student == null) return NotFound();
                student.PhoneNumber = model.PhoneNumber;
                student.Address = model.Address;

                _context.Students.Update(student);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}