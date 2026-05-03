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
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var email = GetUserEmail();
        if (email == null) return Unauthorized();

        var employee = await _employeeService.GetByEmailAsync(email);
        if (employee == null) return NotFound(new { message = "No employee profile is linked to this user email." });

        return Ok(employee);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var employee = await _employeeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var employee = await _employeeService.UpdateAsync(id, dto);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _employeeService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    private string? GetUserEmail() =>
        User.FindFirstValue(ClaimTypes.Email) ??
        User.FindFirstValue(JwtRegisteredClaimNames.Email) ??
        User.FindFirstValue("email");
}
