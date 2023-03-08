using AutoMapper;
using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConsidTechnicalBackend.Services;

public class CategoryService : ICategoryService
{
    private readonly ConsidContext _context;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILibraryItemRepository _libraryItemRepository;
    private readonly IMapper _mapper;
    public CategoryService(
        ConsidContext considContext,
        ICategoryRepository categoryRepository,
        ILibraryItemRepository libraryItemRepository ,
        IMapper mapper
        )
    {
        _context = considContext;
        _categoryRepository = categoryRepository;
        _libraryItemRepository = libraryItemRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateCategoryAsync(string categoryName)
    {
        if (await _categoryRepository.Exists(categoryName))
        {
            var category = _mapper.Map<DbCategory>(categoryName); //Map because we dont want to the obeject to go directly to the database, could be harmfull

            await _categoryRepository.Add(category);
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<bool> UpdateCategoryAsync(UpdateCategoryRequest editCategoryRequest)
    {
        if (await _categoryRepository.Exists(editCategoryRequest.CurrentCategoryName))
        {
            try
            {
                await _categoryRepository.Update(editCategoryRequest.CurrentCategoryName, editCategoryRequest.NewCategoryName); //TODO: Map?
                return true;
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Category not updated");
            }
        }
        else
        {
            return false;
        }
    }
    public async Task<bool> DeleteCategoryAsync(string categoryName)
    {
        if (await _categoryRepository.Exists(categoryName))
        {
            var category = await _categoryRepository.Get(categoryName);

            var libraryItems = await _libraryItemRepository.GetAllByCategoryId(category.Id);

            if (!libraryItems.IsNullOrEmpty())
            {
                return false;
            }

            await _categoryRepository.Delete(categoryName);
            return true;

        }
        else
        {
            return false;
        }
    }
}
