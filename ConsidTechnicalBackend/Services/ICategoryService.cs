using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ICategoryService
{
    Task<bool> CreateCategoryAsync(string categoryName);
    Task<bool> UpdateCategoryAsync(EditCategoryRequest editCategoryRequest);
    Task DeleteCategoryAsync(string categoryName);
}
