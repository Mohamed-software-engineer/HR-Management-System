using System.Runtime.CompilerServices;
using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class ManagerEmployeeConfiguration : IEntityTypeConfiguration<ManagerEmployee>
    {
        public void Configure(EntityTypeBuilder<ManagerEmployee> builder)
        {
            builder.Property(m => m.MonthlySalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(m => m.Bonus)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}