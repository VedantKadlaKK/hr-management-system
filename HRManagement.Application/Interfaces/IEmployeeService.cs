using HRManagement.Application.DTOs;

namespace HRManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto);
    Task<bool> DeleteAsync(int id);
}