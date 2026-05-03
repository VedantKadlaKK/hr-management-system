using HRManagement.Domain.Enums;

namespace HRManagement.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}