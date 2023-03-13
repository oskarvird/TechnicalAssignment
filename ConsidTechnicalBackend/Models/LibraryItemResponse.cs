namespace ConsidTechnicalBackend.Models;

public record LibraryItemResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string? Author { get; set; }
    public int? Pages { get; set; }
    public int? RunTimeMinutes { get; set; }
    public bool IsBorrowable { get; set; }
    public string Borrower { get; set; }
    public DateTime? BorrowDate { get; set; }
    public string Type { get; set; }
}
