using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}