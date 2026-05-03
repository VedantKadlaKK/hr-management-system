using HRManagement.Domain.Enums;

namespace HRManagement.Domain.Entities;

public class LeaveRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType LeaveType { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public string Reason { get; set; } = string.Empty;
    public string? RejectionComment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}