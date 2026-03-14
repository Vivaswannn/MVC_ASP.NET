using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Fees is required")]
        public decimal Fees { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}