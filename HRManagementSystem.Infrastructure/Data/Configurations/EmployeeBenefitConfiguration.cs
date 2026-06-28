using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class EmployeeBenefitConfiguration : IEntityTypeConfiguration<EmployeeBenefit>
    {
        public void Configure(EntityTypeBuilder<EmployeeBenefit> builder)
        {
            builder.HasKey(eb => new { eb.EmployeeId, eb.BenefitId });
            builder.HasIndex(eb => eb.BenefitId);
            builder.HasIndex(eb => new { eb.EmployeeId, eb.IsActive });
            
            builder.Property(eb => eb.EnrollmentDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(eb => eb.IsActive)
                .IsRequired();
            
            builder.ToTable(t => t.HasCheckConstraint(
                    "CK_EmployeeBenefit_EnrollmentDate",
                    "EnrollmentDate <= GETDATE()"
                ));
        }
    }
}