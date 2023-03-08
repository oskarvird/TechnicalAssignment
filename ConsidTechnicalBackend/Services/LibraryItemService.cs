using AutoMapper;
using Azure.Core;
using ConsidTechnicalBackend.Database.Models;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Repositories;

namespace ConsidTechnicalBackend.Services;

public class LibraryItemService : ILibraryItemService
{
    private readonly IMapper _mapper;
    private readonly ILibraryItemRepository _libraryItemRepository;
    public LibraryItemService(
        IMapper mapper, 
        ILibraryItemRepository libraryItemRepository
        )
    {
        _mapper = mapper;
        _libraryItemRepository = libraryItemRepository;
    }

    public async Task CreateLibraryItemAsync(CreateLibraryItemRequest request)
    {
        if ( request == null )
        {
            throw new Exception("Library item values not correct");
        }
        var newLibraryItem = _mapper.Map<DbLibraryItem>(request);

        await _libraryItemRepository.Add(newLibraryItem);
    }
    public async Task<bool> UpdateLibraryItemAsync(UpdateLibraryItemRequest request)
    {
        if (!await _libraryItemRepository.Exists(request.Title))
        {
            return false;
        }
        else
        {
            try
            {
                // map from request
                _mapper.Map<DbLibraryItem>(request);

                //THoughts should we get the libraryitem to pick out then map the new values and insert and save again?



                //TODO: should handle checkout aswell
                //TODO: maybe check in aswell


                var libraryItem = await _libraryItemRepository.Get(request.Title);


                var updateRequestProperties = typeof(UpdateLibraryItemRequest).GetProperties();

                foreach (var property in updateRequestProperties)
                {
                    var newValue = property.GetValue(request);

                    if (newValue != null)
                    {
                        var entityProperty = libraryItem.GetType().GetProperty(property.Name);

                        entityProperty.SetValue(libraryItem, newValue);
                    }
                }


                //await _libraryItemRepository.
                return true;
            }
            catch (Exception)
            {

                throw new BadHttpRequestException("LibraryItem not updated");
            }
        }
    }

    public async Task<bool> DeleteLibraryItemAsync(string title)
    {
        if (!await _libraryItemRepository.Exists(title))
        {
            return false;
        }
        else
        {
            try
            {
                await _libraryItemRepository.Delete(title);
                return true;
            }
            catch (Exception)
            {

                throw new BadHttpRequestException("LibraryItem not deleted");
            }
        }
    }
    public async Task<bool> CheckInLibraryItemAsync(string title)
    {
        if (!await _libraryItemRepository.Exists(title))
        {
            return false;
        }
        else
        {
            try
            {
                var libraryItem = await _libraryItemRepository.Get(title);

                if (libraryItem.IsBorrowable != true
                    && (libraryItem.Type == "Book"
                    || libraryItem.Type == "DVD"
                    || libraryItem.Type == "AudioBook"))
                {

                    libraryItem.IsBorrowable = true;
                    libraryItem.Borrower = ""; 
                    libraryItem.BorrowDate = null;

                    await _libraryItemRepository.Update(libraryItem);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw new BadHttpRequestException("LibraryItem could not be checked in");
            }
        }

        
        
    }
    public async Task CheckOutLibraryItemAsync(string title)
    {
        //TODO: check if item exists then check what type and change IsBorrowed if its not already borrowed
    }

}
