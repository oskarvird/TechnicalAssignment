using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsidTechnicalBackend.Database.Models;

[Table("Category")]
public class DbCategory
{
    public int Id { get; set; }

    [StringLength(100)]
    [Required]
    public string CategoryName { get; set; }
}
