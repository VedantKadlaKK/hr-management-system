using HRManagement.Application.DTOs;
using HRManagement.Application.Interfaces;
using HRManagement.Domain.Entities;
using HRManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Select(e => MapToDto(e))
            .ToListAsync();
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        return employee == null ? null : MapToDto(employee);
    }

    public async Task<EmployeeDto?> GetByEmailAsync(string email)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Email == email);
        return employee == null ? null : MapToDto(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Position = dto.Position,
            Salary = dto.Salary,
            JoiningDate = dto.JoiningDate,
            DepartmentId = dto.DepartmentId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        await _context.Entry(employee).Reference(e => e.Department).LoadAsync();
        return MapToDto(employee);
    }

    public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null) return null;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Phone = dto.Phone;
        employee.Position = dto.Position;
        employee.Salary = dto.Salary;
        employee.IsActive = dto.IsActive;
        employee.DepartmentId = dto.DepartmentId;

        await _context.SaveChangesAsync();
        await _context.Entry(employee).Reference(e => e.Department).LoadAsync();
        return MapToDto(employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    private static EmployeeDto MapToDto(Employee e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        Phone = e.Phone,
        Position = e.Position,
        Salary = e.Salary,
        JoiningDate = e.JoiningDate,
        IsActive = e.IsActive,
        DepartmentId = e.DepartmentId,
        DepartmentName = e.Department?.Name ?? string.Empty
    };
}
