using ConsidTechnicalBackend.Database.Models;

namespace ConsidTechnicalBackend.Repositories;

public interface ILibraryItemRepository
{
    Task Create(DbLibraryItem libraryItem);
    Task<List<DbLibraryItem>> GetAllByCategoryId(int categoryId);
    Task<bool> Exists(string title);
    Task<DbLibraryItem> Get(string title);
    Task Delete(string title);
    Task Update(DbLibraryItem libraryItem);
}
