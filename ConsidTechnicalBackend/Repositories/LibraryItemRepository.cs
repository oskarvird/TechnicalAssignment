using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Repositories;

public class LibraryItemRepository : ILibraryItemRepository
{
    public async Task Create(DbLibraryItem libraryItem)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.LibraryItems.Add(libraryItem);
            await context.SaveChangesAsync();
        }
    }
    public async Task<List<DbLibraryItem>> GetAllByCategoryId(int categoryId)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
