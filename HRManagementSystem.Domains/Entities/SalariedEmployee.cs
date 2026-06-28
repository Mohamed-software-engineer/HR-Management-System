namespace HRManagementSystem.Domains.Entities
{
    public class SalariedEmployee : Employee
    {
        public decimal MonthlySalary { get; set; }

        public override decimal CalculateSalary()
        {
            return MonthlySalary;
        }
    }
}