using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public required string PhoneNumber { get; set;}
        public required string ManagerName { get; set; }
        public required string JobTitle { get; set; }
        public required string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public required string DepartmentName { get; set; }
        public ICollection<string> BenefitNames { get; set; } = new List<string>();
        public decimal? CalculatedSalary{get;set;}

    }
}