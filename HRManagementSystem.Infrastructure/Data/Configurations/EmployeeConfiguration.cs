using HRManagementSystem.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagementSystem.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).
                IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.LastName).
                IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Ignore(e => e.FullName);

            builder.Property(e => e.Gender).
                IsRequired()
                .HasMaxLength(10)
                .HasConversion<string>();

            builder.Property(e => e.JobTitle).
                IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.PhoneNumber).
                IsRequired()
                .HasColumnType("nvarchar(20)");
            builder.HasIndex(e => e.PhoneNumber).
                IsUnique();

            builder.Property(e => e.Email).
                IsRequired()
                .HasColumnType("nvarchar(254)");
            builder.HasIndex(e => e.Email).
                IsUnique();

            builder.Property(e => e.Street).
                IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.City).
                IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.State).
                IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.DateOfBirth).
                IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.DateOfHire).
                IsRequired()
                .HasColumnType("date");

            builder.ToTable(c => c.HasCheckConstraint(
                "CK_Employee_PhoneNumber",
                "PhoneNumber LIKE '+[0-9]%'"
            ));

            builder.ToTable(c => c.HasCheckConstraint(
                "CK_Employee_Email",
                "Email LIKE '%@%_%.__%' AND Email NOT LIKE '%@%@%'"
            ));

            builder.ToTable(c => c.HasCheckConstraint(
                "CK_Employee_HireDate",
                "DateOfHire >= DATEADD(YEAR,18,DateOfBirth)"
            ));
            builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(e => e.DepartmentId);

            builder.HasOne(e => e.Manager)
            .WithMany(m => m.Subordinates)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(e => e.ManagerId);

            builder.HasMany(e => e.EmployeeBenefits)
            .WithOne(eb => eb.Employee)
            .HasForeignKey(eb => eb.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.UseTphMappingStrategy();
            builder.HasDiscriminator<string>("Discriminator")
            .HasValue<ManagerEmployee>("Manager")
            .HasValue<CommissionEmployee>("Commission")
            .HasValue<HourlyEmployee>("Hourly")
            .HasValue<SalariedEmployee>("Salaried");

            // Commission
            builder.ToTable(c => c.HasCheckConstraint(
                    "CK_CommissionEmployee_Rate",
                    "Rate >= 0 AND Rate <= 1"
                ));
            builder.ToTable(c => c.HasCheckConstraint(
                "CK_CommissionEmployee_Target",
                "Target >= 0"
            ));

            // Hourly
            builder.ToTable(c => c.HasCheckConstraint(
                "CK_HourlyEmployee_HourlyRate",
                "HourlyRate >= 0"
            ));
            builder.ToTable(c => c.HasCheckConstraint(
                "CK_HourlyEmployee_HoursWorked",
                "HoursWorked >= 0"
            ));

            // Salaried
            builder.ToTable(c => c.HasCheckConstraint(
               "CK_SalariedEmployee_MonthlySalary",
               "MonthlySalary >= 0"
           ));

            //Manager
            builder.ToTable(c => c.HasCheckConstraint(
                    "CK_ManagerEmployee_MonthlySalary",
                    "MonthlySalary >= 0"
                ));
            builder.ToTable(c => c.HasCheckConstraint(
                "CK_ManagerEmployee_Bonus",
                "Bonus >= 0"
            ));
        }
    }
}
