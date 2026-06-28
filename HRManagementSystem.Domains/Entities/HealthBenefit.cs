namespace HRManagementSystem.Domains.Entities
{
    public class HealthBenefit : Benefit
    {
        public override decimal CalculateCost()
        {
            return Amount * 0.20m;
        }
    }
}