using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}