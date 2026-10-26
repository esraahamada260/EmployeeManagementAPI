using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
private readonly IEmployeeService _employeeService;


public EmployeesController(IEmployeeService employeeService)
{
    _employeeService = employeeService;
}

// GET: api/Employees
[HttpGet]
public async Task<ActionResult<List<Employee>>> GetAll(
    CancellationToken cancellationToken)
{
    var employees = await _employeeService.GetAllAsync(cancellationToken);

    return Ok(employees);
}

// GET: api/Employees/5
[HttpGet("{id:int}")]
public async Task<ActionResult<Employee>> GetById(
    int id,
    CancellationToken cancellationToken)
{
    var employee = await _employeeService.GetByIdAsync(
        id,
        cancellationToken);

    if (employee is null)
    {
        return NotFound(new { message = $"Employee with ID {id} was not found." });
    }

    return Ok(employee);
}

// POST: api/Employees
[HttpPost]
public async Task<ActionResult<Employee>> Create(
[FromBody] CreateEmployeeDto dto,
CancellationToken cancellationToken)
{
var createdEmployee = await _employeeService.CreateAsync(
dto,
cancellationToken);


return CreatedAtAction(
    nameof(GetById),
    new { id = createdEmployee.Id },
    createdEmployee);


}


// PUT: api/Employees/5
[HttpPut("{id:int}")]
public async Task<IActionResult> Update(
int id,
[FromBody] UpdateEmployeeDto dto,
CancellationToken cancellationToken)
{
var updated = await _employeeService.UpdateAsync(
id,
dto,
cancellationToken);


if (!updated)
{
    return NotFound(new
    {
        message = $"Employee with ID {id} was not found."
    });
}

return NoContent();


}


// DELETE: api/Employees/5
[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
{
    var deleted = await _employeeService.DeleteAsync(
        id,
        cancellationToken);

    if (!deleted)
    {
        return NotFound(new { message = $"Employee with ID {id} was not found." });
    }

    return NoContent();
}


}
