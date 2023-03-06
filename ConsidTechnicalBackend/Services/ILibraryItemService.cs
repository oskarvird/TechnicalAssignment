using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ILibraryItemService
{
    Task CreateLibraryItemAsync(CreateLibraryItemRequest request);
    Task UpdateLibraryItemAsync(UpdateLibraryItemRequest request);
    Task<bool> DeleteLibraryItemAsync(string title);
    Task<bool> CheckInLibraryItemAsync(string title);
    Task CheckOutLibraryItemAsync(string title);
}
