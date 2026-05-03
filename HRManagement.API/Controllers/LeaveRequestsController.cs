using HRManagement.Application.DTOs;
using HRManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;
    private readonly IEmployeeService _employeeService;

    public LeaveRequestsController(
        ILeaveRequestService leaveRequestService,
        IEmployeeService employeeService)
    {
        _leaveRequestService = leaveRequestService;
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (IsEmployeeOnly())
        {
            var employee = await GetCurrentEmployeeAsync();
            if (employee == null) return NotFound(new { message = "No employee profile is linked to this user email." });

            var ownLeaves = await _leaveRequestService.GetByEmployeeIdAsync(employee.Id);
            return Ok(ownLeaves);
        }

        var leaves = await _leaveRequestService.GetAllAsync();
        return Ok(leaves);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leave = await _leaveRequestService.GetByIdAsync(id);
        if (leave == null) return NotFound();
        if (IsEmployeeOnly() && !await OwnsLeaveAsync(leave.EmployeeId)) return Forbid();
        return Ok(leave);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        if (IsEmployeeOnly() && !await OwnsLeaveAsync(employeeId)) return Forbid();

        var leaves = await _leaveRequestService.GetByEmployeeIdAsync(employeeId);
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveRequestDto dto)
    {
        if (IsEmployeeOnly())
        {
            var employee = await GetCurrentEmployeeAsync();
            if (employee == null) return NotFound(new { message = "No employee profile is linked to this user email." });
            dto.EmployeeId = employee.Id;
        }

        var leave = await _leaveRequestService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = leave.Id }, leave);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateLeaveStatusDto dto)
    {
        var leave = await _leaveRequestService.UpdateStatusAsync(id, dto);
        if (leave == null) return NotFound();
        return Ok(leave);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _leaveRequestService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    private bool IsEmployeeOnly() =>
        User.IsInRole("Employee") && !User.IsInRole("Admin") && !User.IsInRole("HR");

    private async Task<bool> OwnsLeaveAsync(int employeeId)
    {
        var employee = await GetCurrentEmployeeAsync();
        return employee?.Id == employeeId;
    }

    private async Task<EmployeeDto?> GetCurrentEmployeeAsync()
    {
        var email = GetUserEmail();
        return email == null ? null : await _employeeService.GetByEmailAsync(email);
    }

    private string? GetUserEmail() =>
        User.FindFirstValue(ClaimTypes.Email) ??
        User.FindFirstValue(JwtRegisteredClaimNames.Email) ??
        User.FindFirstValue("email");
}
