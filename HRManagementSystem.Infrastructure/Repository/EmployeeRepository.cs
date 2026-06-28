using HRManagementSystem.Application.Exceptions;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRManagementSystemDbContext _context;
        public EmployeeRepository(HRManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
             _context.Employees.Add(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await FindEmployeeByIdAsync(id);
            _context.Employees.Remove(employee);
        }

        public async Task<ICollection<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Department).Include(e => e.Manager).Include(e => e.EmployeeBenefits).ThenInclude(e => e.Benefit).ToListAsync();
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.Include(e => e.Department).Include(e => e.Manager).Include(e => e.EmployeeBenefits).ThenInclude(e => e.Benefit).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                throw new NotFoundException($"Employee with id {id} not found.");
        
            return employee;
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }
        public async Task<Employee> FindEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                throw new NotFoundException($"Employee with id {id} not found.");
            return employee;
        }

    }
}