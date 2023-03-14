using AutoMapper;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Helpers;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Models.Enums;
using ConsidTechnicalBackend.Repositories;

namespace ConsidTechnicalBackend.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IHelper _calculations;
    public EmployeesService(
        IMapper mapper, 
        IEmployeesRepository employeesRepository,
        IHelper calculations)
    {
        _mapper = mapper;
        _employeesRepository = employeesRepository;
        _calculations = calculations;

    }
    public async Task CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        // Check the criterias for management
        if (request.IsCEO)
        {
            if (await _employeesRepository.CEOExists())
                throw new Exception("CEO already exists");

            if (request.IsManager)
                throw new Exception("CEO cannot be a manager.");

            if (request.ManagerId.HasValue)
                throw new Exception("CEO cannot have a manager.");

        }
        else if (request.IsManager )
        {
            if (request.ManagerId != null || request.ManagerId != 0)
            {
                var manager = await _employeesRepository.GetById(request.ManagerId.Value);

                if (manager == null || !manager.IsManager || !manager.IsCEO)
                    throw new Exception("Manager must have a valid manager/ CEO.");
            }
        }
        else
        {
            if (request.ManagerId != null && request.ManagerId != 0)
            {
                var manager = await _employeesRepository.GetById(request.ManagerId.Value);

                if (manager == null || !manager.IsManager)
                {
                    if (manager.IsCEO)
                    {
                        throw new Exception("Employee cannot have CEO as manager.");
                    }

                    throw new Exception("Employee must have a valid manager.");
                }

            }
        }

        // Map the request to the entity
        var employee = _mapper.Map<DbEmployees>(request);

        // Calculate salary after the checks to avoid unecesary 
        employee.Salary = _calculations.CalculateSalary(request.IsCEO, request.IsManager, request.Rank);

        try
        {
            await _employeesRepository.Add(employee);
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        var employeeToDelete = await _employeesRepository.GetById(employeeId);

        if (employeeToDelete == null)
        {
            throw new Exception("Employee not found");
        
        }
        
        if (employeeToDelete.IsManager || employeeToDelete.IsCEO)
        {
            if(await _employeesRepository.IsManaging(employeeId))
            {
                throw new Exception("Can not delete if managing others");
            }
        }

        try
        {
            await _employeesRepository.Delete(employeeToDelete);
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }

    }
    public async Task UpdateEmployeeAsync(UpdateEmployeeRequest request)
    {

        var employee = _mapper.Map<DbEmployees>(request);

        // Calculate salary
        employee.Salary = _calculations.CalculateSalary(request.IsCEO, request.IsManager, request.Rank);

        if (!await _employeesRepository.Exists(employee.Id))
        {
            throw new InvalidOperationException("Employee not found");
        }

        try
        {
            await _employeesRepository.Update(employee);
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }


    }
    public async Task<EmployeeResponse> GetEmployeeAsync(int id)
    {
        try
        {
            var employee = await _employeesRepository.GetById(id);

            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found");
            }

            var rank = _calculations.CalculateRank(employee.IsCEO, employee.IsManager, employee.Salary);

            var response = _mapper.Map<EmployeeResponse>(employee);

            response.Rank = rank;

            return response;

        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }
    }
    public async Task<List<EmployeeGetResponse>> ListEmployeesAsync()
    {
        try
        {
            var employees = await _employeesRepository.GetAll();

            var roles = Enum.GetNames(typeof(Roles));

            var ceoEmployees = employees.Where(x => x.IsCEO).Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Salary = x.Salary
            }).ToList();

            var managerEmployees = employees.Where(x => x.IsManager && !x.IsCEO).Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Salary = x.Salary
            }).ToList();

            var regularEmployees = employees.Where(x => !x.IsManager && !x.IsCEO).Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Salary = x.Salary
            }).ToList();

            var response = new List<EmployeeGetResponse>();

            if (ceoEmployees.Any())
            {
                response.Add(new EmployeeGetResponse
                {
                    Role = "CEO",
                    Employees = ceoEmployees
                });
            }

            if (managerEmployees.Any())
            {
                response.Add(new EmployeeGetResponse
                {
                    Role = "Managers",
                    Employees = managerEmployees
                });
            }

            if (regularEmployees.Any())
            {
                response.Add(new EmployeeGetResponse
                {
                    Role = "Employees",
                    Employees = regularEmployees
                });
            }

            return response;
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }
       
    }
}