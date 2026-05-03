using HRManagement.Application.DTOs;
using HRManagement.Application.Interfaces;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Application.Services;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly AppDbContext _context;

    public LeaveRequestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllAsync()
    {
        return await _context.LeaveRequests
            .Include(l => l.Employee)
            .Select(l => MapToDto(l))
            .ToListAsync();
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.LeaveRequests
            .Include(l => l.Employee)
            .Where(l => l.EmployeeId == employeeId)
            .Select(l => MapToDto(l))
            .ToListAsync();
    }

    public async Task<LeaveRequestDto?> GetByIdAsync(int id)
    {
        var leave = await _context.LeaveRequests
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(l => l.Id == id);
        return leave == null ? null : MapToDto(leave);
    }

    public async Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto dto)
    {
        var leave = new LeaveRequest
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            LeaveType = dto.LeaveType,
            Reason = dto.Reason,
            EmployeeId = dto.EmployeeId,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeaveRequests.Add(leave);
        await _context.SaveChangesAsync();
        await _context.Entry(leave).Reference(l => l.Employee).LoadAsync();
        return MapToDto(leave);
    }

    public async Task<LeaveRequestDto?> UpdateStatusAsync(int id, UpdateLeaveStatusDto dto)
    {
        var leave = await _context.LeaveRequests
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (leave == null) return null;

        leave.Status = dto.Status;
        leave.RejectionComment = dto.RejectionComment;

        await _context.SaveChangesAsync();
        return MapToDto(leave);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var leave = await _context.LeaveRequests.FindAsync(id);
        if (leave == null) return false;

        _context.LeaveRequests.Remove(leave);
        await _context.SaveChangesAsync();
        return true;
    }

    private static LeaveRequestDto MapToDto(LeaveRequest l) => new()
    {
        Id = l.Id,
        StartDate = l.StartDate,
        EndDate = l.EndDate,
        LeaveType = l.LeaveType.ToString(),
        Status = l.Status.ToString(),
        Reason = l.Reason,
        RejectionComment = l.RejectionComment,
        CreatedAt = l.CreatedAt,
        EmployeeId = l.EmployeeId,
        EmployeeName = l.Employee != null ? $"{l.Employee.FirstName} {l.Employee.LastName}" : string.Empty
    };
}