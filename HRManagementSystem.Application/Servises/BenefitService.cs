using System.IO.Pipelines;
using HRManagementSystem.Application.DTOs.BenefitDTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Application.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IUniteOfWork _uniteOfWork;
        public BenefitService(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        public async Task<int> CreateBenefitAsync(CreateBenefitDTO benefit)
        {
            var entity = CreateBenefitEntity(benefit);

            await _uniteOfWork.BenefitRepository.AddBenefitAsync(entity);
            await _uniteOfWork.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteBenefitAsync(int id)
        {
            await _uniteOfWork.BenefitRepository.DeleteBenefitAsync(id);
            await _uniteOfWork.SaveChangesAsync();
        }

        public async Task<ICollection<BenefitDTO>> GetAllBenefitsAsync()
        {
            var respons = await _uniteOfWork.BenefitRepository.GetAllBenefitsAsync();
            var benefits = respons.Select(b => new BenefitDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Cost = b.CalculateCost(),
            });
            return benefits.ToList();
        }

        public async Task<BenefitDTO> GetBenefitByIdAsync(int id)
        {
            var respons = await _uniteOfWork.BenefitRepository.GetBenefitByIdAsync(id);
            var benefit = new BenefitDTO
            {
              Id = respons.Id,
              Name = respons.Name,
              Description = respons.Description,
              Cost = respons.CalculateCost(),  
            };
            return benefit;
        }

        public async Task UpdateBenefitAsync(int id, UpdatedBenefitDTO benefit)
        {
            var respons = await _uniteOfWork.BenefitRepository.GetBenefitByIdAsync(id);
            if(benefit.Name != null)
                respons.Name = benefit.Name;
            if(benefit.Description != null)
                respons.Description = benefit.Description;
            if(benefit.Amount != null)
                respons.Amount = benefit.Amount.Value;
            await _uniteOfWork.SaveChangesAsync();
        }

        private static Benefit CreateBenefitEntity(CreateBenefitDTO benefit)
        {
            return benefit.BenefitType switch
            {
                BenefitType.Dental => new DentalBenefit
                {
                    Name = benefit.Name,
                    Description = benefit.Description,
                    Amount = benefit.Amount
                },
                BenefitType.Health => new HealthBenefit
                {
                    Name = benefit.Name,
                    Description = benefit.Description,
                    Amount = benefit.Amount
                },
                BenefitType.Vision => new VisionBenefit
                {
                    Name = benefit.Name,
                    Description = benefit.Description,
                    Amount = benefit.Amount
                },
                _ => throw new ArgumentOutOfRangeException(nameof(benefit.BenefitType), benefit.BenefitType, null)
            };
        }
    }
}
