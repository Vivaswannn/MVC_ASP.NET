using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Student List";
            ViewData["Message"] = "Welcome to Student Portal";

            var students = _context.Students
                .Include(s => s.Department)
                .ToList();

            return View(students);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
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
            return View(student);
        }

        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // ---- EDIT GET ----
        public IActionResult Edit(int id)
        {
            var student = _context.Students
                .Where(s => s.StudentId == id)
                .FirstOrDefault();

            if (student == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();
            return View(student);
        }

        // ---- EDIT POST ----
        [HttpPost]
        public IActionResult Edit(Student student, int id)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _context.Departments.ToList();
            return View(student);
        }

        // ---- DELETE GET ----
        public IActionResult Delete(int id)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null) return NotFound();

            return View(student);
        }

        // ---- DELETE POST ----
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ---- SEARCH ----
        public IActionResult Search(string name)
        {
            var students = _context.Students
                .Include(s => s.Department)
                .Where(s => s.Name.Contains(name))
                .ToList();

            ViewBag.Title = $"Search Results for: {name}";
            return View("Index", students); // reuses Index view
        }

        // ---- JSON RESULT ----
        public JsonResult JsonData()
        {
            var students = _context.Students.ToList();
            return Json(students);
        }

        // ---- CONTENT RESULT ----
        public ContentResult Message()
        {
            return Content("Student registered successfully");
        }

        // ---- REDIRECT RESULT ----
        public IActionResult RedirectToDepartment()
        {
            return Redirect("/Department/Index");
        }
    }
}