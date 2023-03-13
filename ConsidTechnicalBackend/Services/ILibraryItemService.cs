using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ILibraryItemService
{
    Task CreateLibraryItemAsync(CreateLibraryItemRequest request);
    Task UpdateLibraryItemAsync(UpdateLibraryItemRequest request);
    Task DeleteLibraryItemAsync(int id);
    Task<List<LibraryItemsByCategoriesResponse>> GetLibraryItemsByCategoriesAsync();
    Task<List<LibraryItemsByTypesResponse>> GetLibraryItemsByTypesAsync();
}
