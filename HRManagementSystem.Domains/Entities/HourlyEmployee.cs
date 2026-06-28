namespace HRManagementSystem.Domains.Entities
{
    public class HourlyEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }

        public override decimal CalculateSalary()
        {
            return HourlyRate * HoursWorked;
        }
    }
}