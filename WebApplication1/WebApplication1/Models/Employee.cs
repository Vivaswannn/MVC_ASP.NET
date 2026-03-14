namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public Employee(int id, string name, string department, decimal salary)
        {
            Id = id;
            Name = name;
            Department = department;
            Salary = salary;
        }
        
        // Parameterless constructor for serialization/deserialization
        public Employee() { }
    }
}
