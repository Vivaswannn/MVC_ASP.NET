using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered");
                    ViewBag.Departments = _context.Departments.ToList();
                    ViewBag.Courses = _context.Courses.ToList();
                    return View(model);
                }

                if (model.Role == "Student")
                {
                    if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone Number is required for students");
                    }
                    if (string.IsNullOrWhiteSpace(model.Address))
                    {
                        ModelState.AddModelError("Address", "Address is required for students");
                    }
                    if (!model.DepartmentId.HasValue || model.DepartmentId.Value == 0)
                    {
                        ModelState.AddModelError("DepartmentId", "Please select a department");
                    }
                    if (!model.CourseId.HasValue || model.CourseId.Value == 0)
                    {
                        ModelState.AddModelError("CourseId", "Please select a course");
                    }

                    if (!ModelState.IsValid)
                    {
                        ViewBag.Departments = _context.Departments.ToList();
                        ViewBag.Courses = _context.Courses.ToList();
                        return View(model);
                    }
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role
                };

                _context.Users.Add(user);

                if (model.Role == "Student")
                {
                    var student = new Student
                    {
                        StudentName = model.FullName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        DepartmentId = model.DepartmentId!.Value,
                        CourseId = model.CourseId!.Value
                    };

                    _context.Students.Add(student);
                }

                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email
                                     && u.Password == model.Password);

                if (user == null)
                {
                   
                    ModelState.AddModelError("", "Invalid email or password");
                    return View(model);
                }

                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserEmail", user.Email);


                if (user.Role == "Teacher")
                    return RedirectToAction("Index", "TeacherDashboard");
                else
                    return RedirectToAction("Index", "StudentDashboard");
            }

            return View(model);
        }

      
        public IActionResult Logout()
        {
            // Clear everything in session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}