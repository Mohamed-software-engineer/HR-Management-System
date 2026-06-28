using HRManagementSystem.Application.Exceptions;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRManagementSystemDbContext _context;
        public DepartmentRepository(HRManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {  
            var department = await FindDepartmentByIdAsync(id);
            _context.Departments.Remove(department);
        }

        public async Task<ICollection<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.Manager).Include(d => d.Employees).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments.Include(d => d.Manager).Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);
            if(department == null)
                throw new NotFoundException($"Department with Id : {id} not found.");
            return department;
        }
        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
        }
        public async Task<Department> FindDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                throw new NotFoundException($"Department with id {id} not found.");
            return department;
        }
    }
}