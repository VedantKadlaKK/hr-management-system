using HRManagement.Domain.Enums;

namespace HRManagement.Application.DTOs;

public class LeaveRequestDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string LeaveType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string? RejectionComment { get; set; }
    public DateTime CreatedAt { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
}

public class CreateLeaveRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType LeaveType { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
}

public class UpdateLeaveStatusDto
{
    public LeaveStatus Status { get; set; }
    public string? RejectionComment { get; set; }
}