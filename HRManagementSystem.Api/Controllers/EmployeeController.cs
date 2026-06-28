using HRManagementSystem.Application.DTOs.EmployeeDTOs;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace HRManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponseDTO>>> GetAll()
        {
            return Ok(await _employeeService.GetAllEmployeesAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> GetById(int id)
        {
            return Ok(await _employeeService.GetEmployeeByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdatedEmployeeDTO dto)
        {
            await _employeeService.UpdateEmployeeAsync(id,dto);
            return Ok();
        }
        [HttpPost("commission")]
        public async Task<ActionResult<int>> CreateCommissionEmployee(CreateCommissionEmployeeDTO dto)
        {
            return Ok(await _employeeService.CreateCommissionEmployeeAsync(dto));
        }
        [HttpPost("hourly")]
        public async Task<ActionResult<int>> CreateHourlyEmployee(CreateHourlyEmployeeDTO dto)
        {
            return Ok(await _employeeService.CreateHourlyEmployeeAsync(dto));
        }
        [HttpPost("salaried")]
        public async Task<ActionResult<int>> CreateSalariedEmployee(CreateSalariedEmployeeDTO dto)
        {
            return Ok(await _employeeService.CreateSalariedEmployeeAsync(dto));
        }
        [HttpPost("manager")]
        public async Task<ActionResult<int>> CreateManagerEmployee(CreateManagerEmployeeDTO dto)
        {
            return Ok(await _employeeService.CreateManagerEmployeeAsync(dto));
        }
        
    }
}