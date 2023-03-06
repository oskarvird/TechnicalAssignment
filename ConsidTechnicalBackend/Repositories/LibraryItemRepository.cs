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
    public async Task<DbLibraryItem> Get(string title)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
    public async Task<List<DbLibraryItem>> GetAllByCategoryId(int categoryId)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
    public async Task<bool> Exists(string title)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.AnyAsync(x => x.Title == title);
        }
    }
    public async Task Update(DbLibraryItem libraryItem)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.LibraryItems.Update(libraryItem);
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(string title)
    {
        using (ConsidContext context = new ConsidContext())
        {
            var entity = await context.LibraryItems.FirstAsync(x => x.Title == title);

            context.LibraryItems.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
