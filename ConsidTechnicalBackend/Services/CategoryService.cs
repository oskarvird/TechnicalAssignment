using AutoMapper;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace ConsidTechnicalBackend.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILibraryItemRepository _libraryItemRepository;
    private readonly IMapper _mapper;
    public CategoryService(
        ICategoryRepository categoryRepository,
        ILibraryItemRepository libraryItemRepository ,
        IMapper mapper
        )
    {
        _categoryRepository = categoryRepository;
        _libraryItemRepository = libraryItemRepository;
        _mapper = mapper;
    }
    public async Task CreateCategoryAsync(string categoryName)
    {

        if (await _categoryRepository.Exists(categoryName))
        {
            throw new Exception("Category already exists");
        }

        try
        {
            var category = _mapper.Map<DbCategory>(categoryName); //Map because we dont want the obeject to go directly in to the database

            await _categoryRepository.Add(category);
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }

    }
    public async Task UpdateCategoryAsync(UpdateCategoryRequest editCategoryRequest)
    {
        if (await _categoryRepository.Exists(editCategoryRequest.Id))
        {
            try
            {
                var category = _mapper.Map<DbCategory>(editCategoryRequest);

                await _categoryRepository.Update(category);
            }
            catch (Exception)
            {
                throw new System.Data.DataException("Error occured while accessing the database, category not updated");
            }
        }
        else
        {
            throw new  Exception("Category do not exist");
        }
    }
    public async Task DeleteCategoryAsync(int id)
    {
        if (await _categoryRepository.Exists(id))
        {
            try
            {
                var category = await _categoryRepository.Get(id);
                var libraryItems = await _libraryItemRepository.GetAllByCategoryId(category.Id);

                if (!libraryItems.IsNullOrEmpty())
                {
                    throw new Exception("Library items connected to the category");
                }

                await _categoryRepository.Delete(category.Id);
            }
            catch (Exception)
            {

                throw new System.Data.DataException("Error occured while accessing the database, category not deleted");
            }
        }
        else
        {
            throw new Exception("Category do not exist");
        }
    }
}