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
    public async Task<DbCategory> Get(string name)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Categories.FirstAsync(x => x.CategoryName == name);
        }
    }
    public async Task<bool> Exists(string name)
    {
        using (ConsidContext context = new ConsidContext())
        {

            return await context.Categories.AnyAsync(x => x.CategoryName == name);

        }
    }
    public async Task Update(string name, string newName)
    {
        using (ConsidContext context = new ConsidContext())
        {
            var response = await context.Categories.FirstAsync(x => x.CategoryName == name);

            response.CategoryName = newName;
            await context.SaveChangesAsync();
        }
    }
    //TODO: Delete have to keep in mind that it dosent have ant library items referenced
    public async Task Delete(string name)
    {
        using (ConsidContext context = new ConsidContext())
        {
            var entity = await context.Categories.FirstAsync(x => x.CategoryName == name);

            context.Categories.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
