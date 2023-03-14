namespace ConsidTechnicalBackend.Models;

public record EmployeeGetResponse
{
    public string Role { get; set; }
    public List<Employee> Employees { get; set; }
}

public record Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
}