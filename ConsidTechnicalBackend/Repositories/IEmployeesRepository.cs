using ConsidTechnicalBackend.Database.Models;

namespace ConsidTechnicalBackend.Repositories;

public interface IEmployeesRepository
{
    Task Add(DbEmployees employee);
    Task<bool> CEOExists();
    Task Delete(DbEmployees employee);
    Task<bool> Exists(int id);
    Task<DbEmployees> GetById(int id);
    Task<bool> IsManaging(int id);
    Task Update(DbEmployees employee);
    Task<List<DbEmployees>> GetAll();
}
