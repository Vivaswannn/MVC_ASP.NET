namespace StudentManagementSystem.ViewModels
{
    public class StudentProfileViewModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public string? DepartmentName { get; set; }
        public string? CourseName { get; set; }
        public int? CourseDuration { get; set; }
        public decimal? CourseFees { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(300)]
        public string Address { get; set; } = string.Empty;
    }
}