using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class HourlyEmployeeConfiguration : IEntityTypeConfiguration<HourlyEmployee>
    {
        public void Configure(EntityTypeBuilder<HourlyEmployee> builder)
        {
            builder.Property(he => he.HourlyRate)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(he => he.HoursWorked)
                .IsRequired();
        }
    }
}