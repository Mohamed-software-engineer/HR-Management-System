using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<ICollection<Employee>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task AddEmployeeAsync(Employee employee);
        public Task UpdateEmployeeAsync(Employee employee);
        public Task DeleteEmployeeAsync(int id);
        public Task<Employee> FindEmployeeByIdAsync(int id);
        
    }
}