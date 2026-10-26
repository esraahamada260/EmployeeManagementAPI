using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services;

public class EmployeeService : IEmployeeService
{
private readonly AppDbContext _context;


public EmployeeService(AppDbContext context)
{
    _context = context;
}

public async Task<List<Employee>> GetAllAsync(
    CancellationToken cancellationToken)
{
    return await _context.Employees
        .AsNoTracking()
        .ToListAsync(cancellationToken);
}

public async Task<Employee?> GetByIdAsync(
    int id,
    CancellationToken cancellationToken)
{
    return await _context.Employees
        .AsNoTracking()
        .FirstOrDefaultAsync(
            employee => employee.Id == id,
            cancellationToken);
}

public async Task<Employee> CreateAsync(
CreateEmployeeDto dto,
CancellationToken cancellationToken)
{
var employee = new Employee
{
FullName = dto.FullName,
Email = dto.Email,
Department = dto.Department,
Salary = dto.Salary,
CreatedAt = DateTime.UtcNow
};


_context.Employees.Add(employee);

await _context.SaveChangesAsync(cancellationToken);

return employee;


}

public async Task<bool> UpdateAsync(
int id,
UpdateEmployeeDto dto,
CancellationToken cancellationToken)
{
var existingEmployee = await _context.Employees
.FirstOrDefaultAsync(
e => e.Id == id,
cancellationToken);


if (existingEmployee is null)
{
    return false;
}

existingEmployee.FullName = dto.FullName;
existingEmployee.Email = dto.Email;
existingEmployee.Department = dto.Department;
existingEmployee.Salary = dto.Salary;

await _context.SaveChangesAsync(cancellationToken);

return true;


}


public async Task<bool> DeleteAsync(
    int id,
    CancellationToken cancellationToken)
{
    var employee = await _context.Employees
        .FirstOrDefaultAsync(
            e => e.Id == id,
            cancellationToken);

    if (employee is null)
    {
        return false;
    }

    _context.Employees.Remove(employee);

    await _context.SaveChangesAsync(cancellationToken);

    return true;
}


}
