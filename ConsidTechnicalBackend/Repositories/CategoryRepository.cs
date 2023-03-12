using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public async Task Add(DbCategory category)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
    }
    public async Task<DbCategory> Get(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
    public async Task<List<DbCategory>> GetAll()
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Categories.ToListAsync();
        }
    }
    public async Task<bool> Exists(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {

            return await context.Categories.AnyAsync(x => x.Id == id);

        }
    }
    public async Task<bool> Exists(string name)
    {
        using (ConsidContext context = new ConsidContext())
        {

            return await context.Categories.AnyAsync(x => x.CategoryName == name);

        }
    }
    public async Task Update(DbCategory category)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            var entity = await context.Categories.FirstAsync(x => x.Id == id);

            context.Categories.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
