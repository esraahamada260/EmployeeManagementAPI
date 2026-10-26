using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs;

public class UpdateEmployeeDto
{
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


}
