using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Infrastructure.Data;

namespace HRManagementSystem.Infrastructure.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly HRManagementSystemDbContext _context;

        public IEmployeeRepository EmployeeRepository{get;}

        public IDepartmentRepository DepartmentRepository {get;}
        public IBenefitRepository BenefitRepository {get;}
        public UniteOfWork(HRManagementSystemDbContext context,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IBenefitRepository benefitRepository)
        {
            _context = context;
            EmployeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
            BenefitRepository = benefitRepository;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}