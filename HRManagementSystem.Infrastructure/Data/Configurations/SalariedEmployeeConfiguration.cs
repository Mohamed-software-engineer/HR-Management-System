using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class SalariedEmployeeConfiguration : IEntityTypeConfiguration<SalariedEmployee>
    {
        public void Configure(EntityTypeBuilder<SalariedEmployee> builder)
        {
            builder.Property(se => se.MonthlySalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}