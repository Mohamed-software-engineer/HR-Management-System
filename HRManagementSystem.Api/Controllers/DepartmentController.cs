using HRManagementSystem.Application.DTOs.DepartmentDTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<DepartmentResponseDTO>>> GetAll()
        {
            return Ok(await _departmentService.GetAllDepartmentsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentResponseDTO>> GetById(int id)
        {
            return Ok(await _departmentService.GetDepartmentByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateDepartmentDTO dto)
        {
            await _departmentService.UpdateDepartmentAsync(id, dto);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDepartmentDTO dto)
        {
            return Ok(await _departmentService.CreateDepartmentAsync(dto));
        }
    }
}