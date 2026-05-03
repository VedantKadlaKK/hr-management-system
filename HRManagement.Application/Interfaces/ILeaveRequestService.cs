using HRManagement.Application.DTOs;

namespace HRManagement.Application.Interfaces;

public interface ILeaveRequestService
{
    Task<IEnumerable<LeaveRequestDto>> GetAllAsync();
    Task<IEnumerable<LeaveRequestDto>> GetByEmployeeIdAsync(int employeeId);
    Task<LeaveRequestDto?> GetByIdAsync(int id);
    Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto dto);
    Task<LeaveRequestDto?> UpdateStatusAsync(int id, UpdateLeaveStatusDto dto);
    Task<bool> DeleteAsync(int id);
}