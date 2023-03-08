namespace ConsidTechnicalBackend.Models;

public class EmployeeGetResponse
{
    public Employee Ceo { get; set; }
    public List<Employee> Managers { get; set; }
    public List<Employee> Employees { get; set; }
}

public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
}