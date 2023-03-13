using AutoMapper;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Helpers;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Models.Enums;
using ConsidTechnicalBackend.Repositories;

namespace ConsidTechnicalBackend.Services;

public class LibraryItemService : ILibraryItemService
{
    private readonly IMapper _mapper;
    private readonly ILibraryItemRepository _libraryItemRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHelper _helper;

    public LibraryItemService(
        IMapper mapper,
        ILibraryItemRepository libraryItemRepository,
        ICategoryRepository categoryRepository,
        IHelper helper
        )
    {
        _mapper = mapper;
        _libraryItemRepository = libraryItemRepository;
        _categoryRepository = categoryRepository;
        _helper = helper;
    }

    public async Task CreateLibraryItemAsync(CreateLibraryItemRequest request)
    {

        if (!await _categoryRepository.Exists(request.CategoryId))
        {
            throw new Exception("Category do not exist");
        }

        DbLibraryItem entity;

        ValidateLibraryItemType(0, request.Type, request.IsBorrowable, request.Borrower, request.BorrowDate, request.Title, request.Author, request.Pages, request.RunTimeMinutes);

        try
        {
            entity = _mapper.Map<DbLibraryItem>(request);

            await _libraryItemRepository.Add(entity);
        }
        catch (Exception)
        {
            throw new System.Data.DataException("Error occured while accessing the database");
        }

    }
    public async Task UpdateLibraryItemAsync(UpdateLibraryItemRequest request)
    {
        DbLibraryItem entity;

        if (!await _libraryItemRepository.Exists(request.Id))
        {
            throw new Exception("Library item does not exist");
        }

        if (!await _categoryRepository.Exists(request.CategoryId))
        {
            throw new Exception("Category do not exist");
        }

        // Check if the requested type is valid

        ValidateLibraryItemType(request.Id, request.Type, request.IsBorrowable, request.Borrower, request.BorrowDate, request.Title, request.Author, request.Pages, request.RunTimeMinutes);

        var databaseObject = await _libraryItemRepository.Get(request.Id);

        if (request.IsBorrowable is true)
        {
            request.Borrower = null;
            request.BorrowDate = null;

        }
        else
        {
            if (!(databaseObject.IsBorrowable == true && databaseObject.Borrower == null && databaseObject.BorrowDate == null))
            {
                throw new Exception("Item need to be checked out before borrowing");
            }
        }

        entity = _mapper.Map<DbLibraryItem>(request);

        try
        {
            await _libraryItemRepository.Update(entity);
        }
        catch (Exception)
        {
            throw new System.Data.DataException("Error occurred while accessing the database");
        }
    }
    public async Task DeleteLibraryItemAsync(int id)
    {
        if (await _libraryItemRepository.Exists(id))
        {
            try
            {
                var entity = await _libraryItemRepository.Get(id);

                await _libraryItemRepository.Delete(entity);
            }
            catch (Exception)
            {
                throw new System.Data.DataException("Error occured while accessing the database");
            }
        }
        else
        {
            throw new Exception("Library item do not exist");
        }
    }
    public async Task<List<LibraryItemsByCategoriesResponse>> GetLibraryItemsByCategoriesAsync()
    {
        var libraryItems = await _libraryItemRepository.GetAll();
        var categories = await _categoryRepository.GetAll();
        var response = new List<LibraryItemsByCategoriesResponse>();

        try
        {
            foreach (var category in categories)
            {
                var libraryItemsInCategories = libraryItems
                .Where(libraryItem => libraryItem.CategoryId == category.Id)
                .Select(libraryItem => new LibraryItemResponse
                {
                    Id = libraryItem.Id,
                    CategoryId = libraryItem.CategoryId,
                    Title = $"{libraryItem.Title} ({_helper.AddAcronym(libraryItem.Title)})",
                    Author = libraryItem.Author,
                    Pages = libraryItem.Pages,
                    RunTimeMinutes = libraryItem.RunTimeMinutes,
                    IsBorrowable = libraryItem.IsBorrowable,
                    Borrower = libraryItem.Borrower,
                    BorrowDate = libraryItem.BorrowDate,
                    Type = libraryItem.Type
                })
                .ToList();

                var libraryItemsByCategory = new LibraryItemsByCategoriesResponse
                {
                    Category = category.CategoryName,
                    LibraryItems = libraryItemsInCategories
                };

                response.Add(libraryItemsByCategory);
            }
        }
        catch (Exception)
        {

            throw new System.Data.DataException("Error occured while accessing the database");
        }

        return response;
    }
    public async Task<List<LibraryItemsByTypesResponse>> GetLibraryItemsByTypesAsync()
    {
        var libraryItems = await _libraryItemRepository.GetAll();

        var types = Enum.GetNames(typeof(Types));

        var response = new List<LibraryItemsByTypesResponse>();

        try
        {
            foreach (var type in types)
            {
                var libraryItemsInCategories = libraryItems
                .Where(libraryItem => libraryItem.Type.Replace(" ", "") == type)
                .Select(libraryItem => new LibraryItemResponse
                {
                    Id = libraryItem.Id,
                    CategoryId = libraryItem.CategoryId,
                    Title = $"{libraryItem.Title} ({_helper.AddAcronym(libraryItem.Title)})",
                    Author = libraryItem.Author,
                    Pages = libraryItem.Pages,
                    RunTimeMinutes = libraryItem.RunTimeMinutes,
                    IsBorrowable = libraryItem.IsBorrowable,
                    Borrower = libraryItem.Borrower,
                    BorrowDate = libraryItem.BorrowDate,
                    Type = libraryItem.Type
                })
                .ToList();

                var libraryItemsByTypes = new LibraryItemsByTypesResponse
                {
                    Type = type,
                    LibraryItems = libraryItemsInCategories
                };

                response.Add(libraryItemsByTypes);

            }
        }
        catch (Exception)
        {
            throw new System.Data.DataException("Error occured while accessing the database");
        }

        return response;
    }
    private static void ValidateLibraryItemType(int? id, string type, bool isBorrowable, string? borrower, DateTime? borrowDate, string title, string? author, int? pages, int? runTimeMinutes)
    {
        if (type == "Reference Book")
        {
            if (isBorrowable || borrower != null || borrowDate != null)
            {
                throw new Exception("Invalid parameters for reference book type, cant be borrowed");
            }

            if (id == null || title == null || author == null || pages == null)  //Checking the criterias for reference book
            {
                throw new Exception($"Not all parameters are set right for {type}");
            }

        }
        else if (type == "Book" || type == "DVD" || type == "Audio Book")
        {

            if (type == "Book")
            {
                if (id == null || title == null || author == null || pages == null) //Checking the criterias for book
                {
                    throw new Exception($"Not all parameters are set right for {type}");
                }
            }
            else if (type == "DVD" || type == "Audio Book")
            {
                if (id == null || title == null || runTimeMinutes == null) //Checking the criterias for dvd and audio book
                {
                    throw new Exception($"Not all parameters are set right for {type}");
                }
            }

            bool isValid = (isBorrowable != null && borrower != null && borrowDate != null)
                        || (isBorrowable  == null && borrower == null && borrowDate == null);

            if (!isValid)
            {
                throw new Exception("Error: IsBorrowable, Borrower and BorrowDate must all have values or be null");
            }
        }
        else
        {
            throw new Exception("Invalid library item type");
        }
    }
}