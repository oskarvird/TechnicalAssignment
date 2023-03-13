namespace ConsidTechnicalBackend.Models;

public record EmployeeGetResponse
{
    public string Role { get; set; }
    public List<EmployeeResponse> Employees { get; set; }
}

public record EmployeeResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
}