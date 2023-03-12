namespace ConsidTechnicalBackend.Models;

public record UpdateCategoryRequest
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
}
