using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsidTechnicalBackend.Database.Models;

[Table("LibraryItem")]
public class DbLibraryItem
{
    public int Id { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public DbCategory Category { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Author { get; set; }

    public int? Pages { get; set; }

    public int? RunTimeMinutes { get; set; }

    [Required]
    public bool IsBorrowable { get; set; }

    public string? Borrower { get; set; }

    public DateTime? BorrowDate { get; set; }

    [Required]
    public string Type { get; set; }
}