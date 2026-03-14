using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
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

            var students = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ---- BONUS: Search ----
        public IActionResult Search(string name, int? departmentId)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var students = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
                students = students.Where(s => s.StudentName.Contains(name));

            if (departmentId.HasValue)
                students = students.Where(s => s.DepartmentId == departmentId);

            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.SearchName = name;
            ViewBag.SelectedDepartment = departmentId;

            return View("Index", students.ToList());
        }
    }
}