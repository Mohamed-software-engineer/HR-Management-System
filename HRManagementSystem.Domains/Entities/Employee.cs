using System.ComponentModel.DataAnnotations;
using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Domains.Entities
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public required Gender Gender { get; set; }
        public required string JobTitle { get; set; }
        public required string PhoneNumber { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public string Address => $"{Street}, {City}, {State}";
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? ManagerId { get; set; }
        public ManagerEmployee? Manager { get; set; }

        public abstract decimal CalculateSalary();

    }
}