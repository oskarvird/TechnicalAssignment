using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Repositories;

public class LibraryItemRepository : ILibraryItemRepository
{

    public async Task Add(DbLibraryItem libraryItem)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.LibraryItems.Add(libraryItem);
            await context.SaveChangesAsync();
        }
    }

    public async Task<DbLibraryItem> Get(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task<List<DbLibraryItem>> GetAllByCategoryId(int categoryId)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }

    public async Task<bool> Exists(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.AnyAsync(x => x.Id == id);
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

    public async Task Delete(DbLibraryItem libraryItem)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.LibraryItems.Remove(libraryItem);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<DbLibraryItem>> GetAll()
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.LibraryItems.ToListAsync();
        }
    }
}
