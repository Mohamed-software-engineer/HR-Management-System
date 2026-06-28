using HRManagementSystem.Application.DTOs.BenefitDTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitService _benefitService;
        public BenefitController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BenefitDTO>>> GetAll()
        {
            return Ok(await _benefitService.GetAllBenefitsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BenefitDTO>> GetById(int id)
        {
            return Ok(await _benefitService.GetBenefitByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _benefitService.DeleteBenefitAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdatedBenefitDTO dto)
        {
            await _benefitService.UpdateBenefitAsync(id, dto);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateBenefitDTO dto)
        {
            return Ok(await _benefitService.CreateBenefitAsync(dto));
        }
    }
}