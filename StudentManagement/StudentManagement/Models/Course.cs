using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public int Duration { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}