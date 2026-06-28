using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Infrastructure.Data
{
    public class HRManagementSystemDbContext : DbContext
    {
        public HRManagementSystemDbContext(DbContextOptions<HRManagementSystemDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(HRManagementSystemDbContext).Assembly);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
