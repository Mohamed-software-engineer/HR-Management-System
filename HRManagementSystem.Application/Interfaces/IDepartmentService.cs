using HRManagementSystem.Application.DTOs.DepartmentDTOs;
using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IDepartmentService
    {
        public Task<int> CreateDepartmentAsync(CreateDepartmentDTO department);
        public Task<ICollection<DepartmentResponseDTO>> GetAllDepartmentsAsync();
        public Task<DepartmentResponseDTO> GetDepartmentByIdAsync(int id);
        public Task UpdateDepartmentAsync(int id, UpdateDepartmentDTO department);
        public Task DeleteDepartmentAsync(int id);
    }
}