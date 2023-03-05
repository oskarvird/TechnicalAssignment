using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ILibraryItemService
{
    Task CreateLibraryItemAsync(CreateLibraryItemRequest request);
    Task UpdateLibraryItemAsync(UpdateLibraryItemRequest request);
}
