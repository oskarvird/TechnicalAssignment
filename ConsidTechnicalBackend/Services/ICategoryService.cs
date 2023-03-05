using ConsidTechnicalBackend.Models;

namespace ConsidTechnicalBackend.Services;

public interface ICategoryService
{
    Task<bool> CreateCategoryAsync(string categoryName);
    Task<bool> UpdateCategoryAsync(UpdateCategoryRequest editCategoryRequest);
    Task<bool> DeleteCategoryAsync(string categoryName);
}
