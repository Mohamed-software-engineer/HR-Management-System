namespace HRManagementSystem.Application.Interfaces
{
    public interface IUniteOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IBenefitRepository BenefitRepository { get; }
        public Task SaveChangesAsync();
    }
}