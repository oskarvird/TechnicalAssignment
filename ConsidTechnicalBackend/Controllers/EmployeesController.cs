using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidTechnicalBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService _employeesService;
    public EmployeesController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpPut("create")]
    public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {

        await _employeesService.CreateEmployeeAsync(createEmployeeRequest);

            return Ok();

    }
    [HttpPut("update")]
    public async Task<ActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest updateEmployeeRequest)
    {

        await _employeesService.UpdateEmployeeAsync(updateEmployeeRequest);

        return Ok();

    }
    [HttpPut("delete")]
    public async Task<ActionResult> DeleteEmployee([FromQuery] int employeeId)
    {

        await _employeesService.DeleteEmployeeAsync(employeeId);

        return Ok();

    }
    [HttpPut("list")]
    public async Task<ActionResult> GetEmployees()
    {
        var listOfEmployees = await _employeesService.ListEmployeesAsync();

        return Ok(listOfEmployees);
    }
}
