using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagement.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");
        builder.HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}