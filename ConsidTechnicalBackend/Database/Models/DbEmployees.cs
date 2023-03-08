using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsidTechnicalBackend.Database.Models;

[Table("Employees")]
public class DbEmployees
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public decimal Salary { get; set; }

    [Required]
    public bool IsCEO { get; set; }

    [Required]
    public bool IsManager { get; set; }

    public int? ManagerId { get; set; }
}