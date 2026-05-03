using HRManagement.Application.DTOs;
using HRManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var employee = await _employeeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var employee = await _employeeService.UpdateAsync(id, dto);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _employeeService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}