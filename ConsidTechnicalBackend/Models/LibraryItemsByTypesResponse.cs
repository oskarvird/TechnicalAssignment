namespace ConsidTechnicalBackend.Models;

public record LibraryItemsByTypesResponse 
{
    public string Type { get; set; }
    public List<LibraryItemResponse> LibraryItems { get; set; }
}