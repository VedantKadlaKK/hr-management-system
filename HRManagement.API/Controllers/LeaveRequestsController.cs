using HRManagement.Application.DTOs;
using HRManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leaves = await _leaveRequestService.GetAllAsync();
        return Ok(leaves);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leave = await _leaveRequestService.GetByIdAsync(id);
        if (leave == null) return NotFound();
        return Ok(leave);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(int employeeId)
    {
        var leaves = await _leaveRequestService.GetByEmployeeIdAsync(employeeId);
        return Ok(leaves);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveRequestDto dto)
    {
        var leave = await _leaveRequestService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = leave.Id }, leave);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateLeaveStatusDto dto)
    {
        var leave = await _leaveRequestService.UpdateStatusAsync(id, dto);
        if (leave == null) return NotFound();
        return Ok(leave);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _leaveRequestService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}