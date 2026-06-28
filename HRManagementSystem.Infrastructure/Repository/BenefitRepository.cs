using HRManagementSystem.Application.Exceptions;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using HRManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Repository
{
    public class BenefitRepository : IBenefitRepository
    {
        private readonly HRManagementSystemDbContext _context;
        public BenefitRepository(HRManagementSystemDbContext context)
        {
            _context = context;   
        }

        public async Task AddBenefitAsync(Benefit benefit)
        {
            _context.Benefits.Add(benefit);
        }

        public async Task DeleteBenefitAsync(int id)
        {
            var benefit = await FindBenefitByIdAsync(id);
            _context.Benefits.Remove(benefit);
        }

        public async Task<ICollection<Benefit>> GetAllBenefitsAsync()
        {
            return await _context.Benefits.ToListAsync();
        }

        public async Task<Benefit> GetBenefitByIdAsync(int id)
        {
            var benefit = await FindBenefitByIdAsync(id);
            return benefit;
        }

        public async Task UpdateBenefitAsync(Benefit benefit)
        {
            await FindBenefitByIdAsync(benefit.Id);
            _context.Benefits.Update(benefit);
        }
        public async Task<Benefit> FindBenefitByIdAsync(int id)
        {
            var benefit = await _context.Benefits.FindAsync(id);
            if (benefit == null)
                throw new NotFoundException($"Benefit with id {id} not found.");
            return benefit;
        }
    }
}