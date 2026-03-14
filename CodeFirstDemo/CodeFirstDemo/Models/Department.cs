using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CodeFirstDemo.Models
{
    public class Department
    {
        [Key]
        public int DeptID { get; set; }

        [Required]
        [StringLength(100)]

        public string DeptName { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
