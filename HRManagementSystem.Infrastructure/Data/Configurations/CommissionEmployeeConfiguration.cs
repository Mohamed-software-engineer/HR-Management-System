using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class CommissionEmployeeConfiguration : IEntityTypeConfiguration<CommissionEmployee>
    {
        public void Configure(EntityTypeBuilder<CommissionEmployee> builder)
        {
            builder.Property(ce => ce.Rate)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(ce => ce.Target)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}