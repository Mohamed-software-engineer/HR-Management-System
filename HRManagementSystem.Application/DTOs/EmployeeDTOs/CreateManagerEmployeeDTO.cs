namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class CreateManagerEmployeeDTO : CreateEmployeeDTO
    {
        public decimal MonthlySalary { get; set; }
        public decimal Bonus { get; set; }
    }
}