using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsTeacher()
        {
            return HttpContext.Session.GetString("UserRole") == "Teacher";
        }

        public IActionResult Index()
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var courses = _context.Courses
                .Include(c => c.Department)
                .ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.CourseId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var course = _context.Courses
                .Include(c => c.Department)
                .FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}