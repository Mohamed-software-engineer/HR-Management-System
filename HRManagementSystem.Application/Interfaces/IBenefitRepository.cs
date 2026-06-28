using HRManagementSystem.Domains.Entities;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IBenefitRepository
    {
        public Task <ICollection<Benefit>> GetAllBenefitsAsync();
        public Task<Benefit> GetBenefitByIdAsync(int id);
        public Task AddBenefitAsync(Benefit benefit);
        public Task UpdateBenefitAsync(Benefit benefit);
        public Task DeleteBenefitAsync(int id);
        public Task<Benefit> FindBenefitByIdAsync(int id);
    }
}