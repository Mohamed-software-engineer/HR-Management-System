using HRManagementSystem.Application.DTOs.EmployeeDTOs;
using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<int> CreateHourlyEmployeeAsync(CreateHourlyEmployeeDTO dto);
        public Task<int> CreateCommissionEmployeeAsync(CreateCommissionEmployeeDTO dto);
        public Task<int> CreateSalariedEmployeeAsync(CreateSalariedEmployeeDTO dto);
        public Task<int> CreateManagerEmployeeAsync(CreateManagerEmployeeDTO dto);
        public Task<ICollection<EmployeeResponseDTO>> GetAllEmployeesAsync();
        public Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);
        public Task UpdateEmployeeAsync(int id, UpdatedEmployeeDTO employee);
        public Task DeleteEmployeeAsync(int id);    
    }
}
