namespace HRManagementSystem.Domains.Entities
{
    public class CommissionEmployee : Employee
    {
        public decimal Rate { get; set; }
        public decimal Target { get; set; }

        public override decimal CalculateSalary()
        {
            return Rate * Target;
        }
    }
}