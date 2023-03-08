using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    public async Task Add(DbEmployees employee)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
    public async Task<bool> CEOExists()
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Employees.AnyAsync(x => x.IsCEO == true);
        }
    }
    public async Task<DbEmployees> GetById(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            //TODO: if (employee == null)
            //{
            //    throw new ArgumentException($"Employee with id {id} not found.");
            //}

            return employee;
        }
    }
    public async Task<bool> IsManaging(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Employees.AnyAsync(x => x.ManagerId == id);
        }
    }
    public async Task Update(DbEmployees employee)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }
    }
    public async Task Delete(DbEmployees employee)
    {
        using (ConsidContext context = new ConsidContext())
        {
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }
    }
    public async Task<bool> Exists(int id)
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Employees.AnyAsync(x => x.Id == id);
        }
    }
    public async Task<List<DbEmployees>> GetAll()
    {
        using (ConsidContext context = new ConsidContext())
        {
            return await context.Employees.ToListAsync();
        }
    }
}
