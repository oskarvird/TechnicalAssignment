using ConsidTechnicalBackend.Database.Models;

namespace ConsidTechnicalBackend.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task Add(DbCategory category);
        Task Update(DbCategory category);
        Task Delete(int id);
        Task<DbCategory> Get(int id);
        Task<List<DbCategory>> GetAll();
    }
}
