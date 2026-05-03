using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagement.Infrastructure.Data.Configurations;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Reason).IsRequired().HasMaxLength(500);
        builder.Property(l => l.RejectionComment).HasMaxLength(500);
        builder.Property(l => l.LeaveType).HasConversion<string>();
        builder.Property(l => l.Status).HasConversion<string>();
        builder.HasOne(l => l.Employee)
               .WithMany(e => e.LeaveRequests)
               .HasForeignKey(l => l.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}