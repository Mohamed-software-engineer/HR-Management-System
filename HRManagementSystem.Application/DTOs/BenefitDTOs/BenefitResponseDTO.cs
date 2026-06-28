namespace HRManagementSystem.Application.DTOs.BenefitDTOs
{
    public class BenefitDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }

    }
}