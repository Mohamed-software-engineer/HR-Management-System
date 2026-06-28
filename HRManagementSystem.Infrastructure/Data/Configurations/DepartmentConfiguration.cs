using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.HasIndex(d => d.ManagerId)
                .IsUnique();

            builder.Property(d => d.Description)
                .HasColumnType("nvarchar(500)");

            builder.HasOne(d => d.Manager)
                .WithOne()
                .HasForeignKey<Department>(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable(
                c => c.HasCheckConstraint(
                    "CK_Department_Name",
                    "LEN(TRIM(Name)) > 0"
                )
            );
        }
    }
}
