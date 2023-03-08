using ConsidTechnicalBackend.Database.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsidTechnicalBackend.Models;

public class CreateLibraryItemRequest
{
    public int CategoryId { get; set; }
    public DbCategory Category { get; set; } //TODO: kommer vi ta in id för kategori eller namnet?
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsBorrowable { get; set; }
    public string Borrower { get; set; }
    public string Type { get; set; } 
}
