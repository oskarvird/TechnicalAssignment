using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface IEmployeesService
{
    Task CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest);
    Task DeleteEmployeeAsync(int employeeId);
    Task UpdateEmployeeAsync(UpdateEmployeeRequest updateEmployeeRequest);
    Task<List<EmployeeGetResponse>> ListEmployeesAsync();
}
