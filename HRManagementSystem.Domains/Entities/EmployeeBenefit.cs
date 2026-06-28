namespace HRManagementSystem.Domains.Entities
{
    public class EmployeeBenefit
    {
        public int EmployeeId { get; set; }
        public int BenefitId { get; set; }
        public bool IsActive { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Employee Employee { get; set; } = null!;
        public Benefit Benefit { get; set; } = null!;
    }
}