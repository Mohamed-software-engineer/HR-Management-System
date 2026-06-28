using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class BenefitConfiguration : IEntityTypeConfiguration<Benefit>
    {
        public void Configure(EntityTypeBuilder<Benefit> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            
            builder.HasIndex(b => b.Name)
                .IsUnique();

            builder.Property(b => b.Description)
                .HasColumnType("nvarchar(500)");

            builder.Property(b => b.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(b => b.EmployeeBenefits)
                .WithOne(eb => eb.Benefit)
                .HasForeignKey(eb => eb.BenefitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.UseTphMappingStrategy();
            builder.HasDiscriminator<string>("BenefitType")
            .HasValue<DentalBenefit>("Dental")
            .HasValue<HealthBenefit>("Health")
            .HasValue<VisionBenefit>("Vision");

            builder.Property("BenefitType")
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex("BenefitType");
            builder.ToTable(
                c => c.HasCheckConstraint(
                    "CK_Benefit_Amount",
                    "Amount >= 0"
                )
            );
        }
    }
}