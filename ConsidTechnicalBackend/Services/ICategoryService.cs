using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ICategoryService
{
    Task CreateCategoryAsync(string categoryName);
    Task UpdateCategoryAsync(UpdateCategoryRequest editCategoryRequest);
    Task DeleteCategoryAsync(int id);
}
