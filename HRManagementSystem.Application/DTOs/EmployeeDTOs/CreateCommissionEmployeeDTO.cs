namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class CreateCommissionEmployeeDTO : CreateEmployeeDTO
    {
        public decimal Rate {get; set;}
        public decimal Target {get; set;}
    }
}