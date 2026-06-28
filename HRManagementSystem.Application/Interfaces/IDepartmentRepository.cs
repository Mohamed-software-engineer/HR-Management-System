using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<ICollection<Department>> GetAllDepartmentsAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task AddDepartmentAsync(Department department);
        public Task UpdateDepartmentAsync(Department department);
        public Task DeleteDepartmentAsync(int id);
        public Task<Department> FindDepartmentByIdAsync(int id);
    }
}