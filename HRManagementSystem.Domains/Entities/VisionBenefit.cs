namespace HRManagementSystem.Domains.Entities
{
    public class VisionBenefit : Benefit
    {
        public override decimal CalculateCost()
        {
            return Amount * 0.15m;
        }
    }
}