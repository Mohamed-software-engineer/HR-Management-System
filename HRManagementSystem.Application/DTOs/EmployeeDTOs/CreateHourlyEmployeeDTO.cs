namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class CreateHourlyEmployeeDTO : CreateEmployeeDTO
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
    }
}