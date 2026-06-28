namespace HRManagementSystem.Domains.Entities
{
    public class DentalBenefit : Benefit
    {
        public override decimal CalculateCost()
        {
             return Amount * 0.10m;
        }   
    }
}