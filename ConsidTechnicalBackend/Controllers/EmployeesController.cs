using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
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

    [HttpPost("create")]
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

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteEmployee([FromQuery] int id)
    {

        await _employeesService.DeleteEmployeeAsync(id);
        return Ok();

    }

    [HttpGet("list")]
    public async Task<ActionResult> ListEmployees()
    {

        var listOfEmployees = await _employeesService.ListEmployeesAsync();
        return Ok(listOfEmployees);

    }
}
