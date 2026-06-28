using HRManagementSystem.Application.DTOs.BenefitDTOs;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IBenefitService
    {
        public Task<int> CreateBenefitAsync(CreateBenefitDTO benefit);
        public Task<ICollection<BenefitDTO>> GetAllBenefitsAsync();
        public Task<BenefitDTO> GetBenefitByIdAsync(int id);
        public Task UpdateBenefitAsync(int id, UpdatedBenefitDTO benefit);
        public Task DeleteBenefitAsync(int id);
    }
}
