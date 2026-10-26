using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models;

public class Employee
{
public int Id { get; set; }


[Required]
[MaxLength(100)]
public string FullName { get; set; } = string.Empty;

[Required]
[EmailAddress]
[MaxLength(150)]
public string Email { get; set; } = string.Empty;

[Required]
[MaxLength(100)]
public string Department { get; set; } = string.Empty;

[Range(0, 100000000)]
public decimal Salary { get; set; }

public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}
