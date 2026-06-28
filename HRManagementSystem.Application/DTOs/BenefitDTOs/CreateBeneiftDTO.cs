using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Application.DTOs.BenefitDTOs
{
    public class CreateBenefitDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public required BenefitType BenefitType { get; set; }
    }
}
