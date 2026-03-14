using System.Reflection.Metadata.Ecma335;
using EmployeeApp.Models;
using System.Collections.Generic;

namespace EmployeeApp.Repositories
{
    public class EmployeeRepository
    {
        private static List<Employee> allEmployees = new List<Employee>();

        public static IEnumerable<Employee> AllEmployees()
        {
            return allEmployees;
        }
        public static void Create(Employee employee)
        {
            allEmployees.Add(employee);
        }
    }
}
