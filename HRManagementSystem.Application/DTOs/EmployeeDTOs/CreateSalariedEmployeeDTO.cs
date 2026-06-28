namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class CreateSalariedEmployeeDTO : CreateEmployeeDTO
    {
        public decimal MonthlySalary { get; set; }
    }
}