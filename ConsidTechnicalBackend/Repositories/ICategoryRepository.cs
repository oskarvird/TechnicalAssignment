using ConsidTechnicalBackend.Database.Models;

namespace ConsidTechnicalBackend.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> Exists(string name);
        Task Add(DbCategory category);
        Task Update(string name, string newName);
        Task Delete(string name);
        Task<DbCategory> Get(string name);
    }
}
