using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Application.DTOs.EmployeeDTOs
{
    public class CreateEmployeeDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public required string JobTitle { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
        public ICollection<int>? BenefitIds { get; set; }

    }
}
