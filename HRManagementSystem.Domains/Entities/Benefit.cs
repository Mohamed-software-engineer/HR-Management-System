namespace HRManagementSystem.Domains.Entities
{
    public abstract class Benefit
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
        public abstract decimal CalculateCost();
    }
}