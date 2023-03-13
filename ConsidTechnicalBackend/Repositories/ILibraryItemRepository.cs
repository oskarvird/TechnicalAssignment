using ConsidTechnicalBackend.Database.Models;

namespace ConsidTechnicalBackend.Repositories;

public interface ILibraryItemRepository
{
    Task Add(DbLibraryItem libraryItem);
    Task<List<DbLibraryItem>> GetAllByCategoryId(int categoryId);
    Task<bool> Exists(int id);
    Task<DbLibraryItem> Get(int id);
    Task Delete(DbLibraryItem libraryItem);
    Task Update(DbLibraryItem libraryItem);
    Task<List<DbLibraryItem>> GetAll();
}
