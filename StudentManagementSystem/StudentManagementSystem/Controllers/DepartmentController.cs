using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Only teachers can access — check every action
        private bool IsTeacher()
        {
            return HttpContext.Session.GetString("UserRole") == "Teacher";
        }

        public IActionResult Index()
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var departments = _context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var department = _context.Departments.Find(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            if (!IsTeacher()) return RedirectToAction("Login", "Account");

            var department = _context.Departments.Find(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}