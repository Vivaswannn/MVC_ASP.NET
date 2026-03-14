using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeFirstDemo.Context;
using CodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstDemo.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext ?? throw new ArgumentNullException(nameof(employeeContext));
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _employeeContext.Employees.FindAsync(id);
        }

        public async Task AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            await _employeeContext.Employees.AddAsync(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _employeeContext.Employees.Update(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee == null) return;
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
        }
    }
}
