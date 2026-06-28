namespace HRManagementSystem.Domains.Entities
{
    public class ManagerEmployee : Employee
    {
        public decimal MonthlySalary { get; set; }
        public decimal Bonus { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        public override decimal CalculateSalary()
        {
            return MonthlySalary + Bonus;
        }
    }
}