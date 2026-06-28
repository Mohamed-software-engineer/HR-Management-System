using HRManagementSystem.Application.DTOs.DepartmentDTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUniteOfWork _uniteOfWork;
        public DepartmentService(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }
        public async Task<int> CreateDepartmentAsync(CreateDepartmentDTO department)
        {
            var newDepartment = new Department
            {
              Name = department.Name,
              Description = department.Description,
              ManagerId = department.ManagerId,  
            };
            await _uniteOfWork.DepartmentRepository.AddDepartmentAsync(newDepartment);
            await _uniteOfWork.SaveChangesAsync();
            return newDepartment.Id;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _uniteOfWork.DepartmentRepository.DeleteDepartmentAsync(id);
            await _uniteOfWork.SaveChangesAsync();
        }

        public async Task<ICollection<DepartmentResponseDTO>> GetAllDepartmentsAsync()
        {
            var response = await _uniteOfWork.DepartmentRepository.GetAllDepartmentsAsync();
            var departments = response.Select(d => new DepartmentResponseDTO
            {
               Name = d.Name,
               Description = d.Description != null? d.Description : " ",
               ManagerName = d.Manager != null? d.Manager.FullName : " ",
               EmployeeNames = d.Employees.Select(e => e.FullName).ToList(),
            });
            return departments.ToList();
        }

        public async Task<DepartmentResponseDTO> GetDepartmentByIdAsync(int id)
        {
            var response = await _uniteOfWork.DepartmentRepository.GetDepartmentByIdAsync(id);
            var department = new DepartmentResponseDTO
            {
                Name = response.Name,
                Description = response.Description,
                ManagerName = response.Manager != null? response.Manager.FullName : " ",
                EmployeeNames = response.Employees.Select(e => e.FullName).ToList()
            };

            return department;
        }

        public async Task UpdateDepartmentAsync(int id,UpdateDepartmentDTO department)
        {
            var response = await _uniteOfWork.DepartmentRepository.GetDepartmentByIdAsync(id);
            if(department.Name != null)
                response.Name = department.Name;
            if(department.Description != null)
                response.Description = department.Description;
            if(department.ManagerId != null)
                response.ManagerId = department.ManagerId;
            await _uniteOfWork.SaveChangesAsync();
        }
    }
}