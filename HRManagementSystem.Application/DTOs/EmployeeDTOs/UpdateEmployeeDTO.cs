namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class UpdatedEmployeeDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfHire { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
        public List<int>? BenefitIds { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Target { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? HoursWorked { get; set; }
        public decimal? MonthlySalary { get; set; }
        public decimal? Bonus { get; set; }
    }
}