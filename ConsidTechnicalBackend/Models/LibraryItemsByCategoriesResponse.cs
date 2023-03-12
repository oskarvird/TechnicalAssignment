namespace ConsidTechnicalBackend.Models;

public record LibraryItemsByCategoriesResponse
{
    public string Category { get; set; }
    public List<LibraryItemResponse> LibraryItems { get; set; }
}
