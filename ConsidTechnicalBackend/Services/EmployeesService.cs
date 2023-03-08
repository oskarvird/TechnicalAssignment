using AutoMapper;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Helpers;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Repositories;

namespace ConsidTechnicalBackend.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly ICalculations _calculations;
    public EmployeesService(
        IMapper mapper, 
        IEmployeesRepository employeesRepository,
         ICalculations calculations)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
        _calculations = calculations;

    }
    public async Task CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        // Map the request to the entity
        var employee = _mapper.Map<DbEmployees>(request);

        if (request.IsCEO)
        {
            if (await _employeesRepository.CEOExists())
            {
                throw new InvalidOperationException("CEO already exists");
            }
            if (request.IsManager)
            {
                throw new InvalidOperationException("CEO cannot be a manager.");
            }
            if (request.ManagerId.HasValue)
            {
                throw new InvalidOperationException("CEO cannot have a manager.");
            }
        }
        else if (request.IsManager)
        {
            var manager = _employeesRepository.GetById(request.ManagerId.Value).Result;
            if (manager == null || !manager.IsManager)
            {
                throw new InvalidOperationException("Manager must have a valid manager.");
            }
        }
        else if(!request.IsManager && !request.IsCEO)
        {
            var manager = _employeesRepository.GetById(request.ManagerId.Value).Result;
            if (manager == null || !manager.IsManager)
            {
                throw new InvalidOperationException("Employee must have a valid manager.");
            }
            if (manager.IsCEO)
            {
                throw new InvalidOperationException("Employee cannot have CEO as manager.");
            }
        }
        
        // Calculate salary after the checks to avoid unecesary ..
        employee.Salary = _calculations.CalculateSalary(request.IsCEO, request.IsManager, request.Rank);


        await _employeesRepository.Add(employee);
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        var employeeToDelete = await _employeesRepository.GetById(employeeId);

        if (employeeToDelete == null)
        {
            throw new InvalidOperationException("Employee not found");
        
        }
        
        if (employeeToDelete.IsManager || employeeToDelete.IsCEO)
        {
            if(await _employeesRepository.IsManaging(employeeId))
            {
                throw new InvalidOperationException("Can not delete if managing");
            }
        }

        await _employeesRepository.Delete(employeeToDelete);
    }
    public async Task UpdateEmployeeAsync(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var employee = _mapper.Map<DbEmployees>(updateEmployeeRequest);

        if (!await _employeesRepository.Exists(employee.Id))
        {
            throw new InvalidOperationException("Employee not found");
        }

        await _employeesRepository.Update(employee);

    }
    public async Task<EmployeeGetResponse> ListEmployeesAsync()
    {
        var employees = await _employeesRepository.GetAll();

        EmployeeGetResponse response = new EmployeeGetResponse(); //Split to three objects/ lists to easily destinct the diffrent roles 

        foreach (var employee in employees)
        {
            if(employee.IsCEO)
            {
                response.Ceo = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                };
            }
            else if (employee.IsManager)
            {
                response.Managers.Add(new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                });
            }
            else
            {
                response.Employees.Add(new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                });
            }
        }

        return response;
    }
}
