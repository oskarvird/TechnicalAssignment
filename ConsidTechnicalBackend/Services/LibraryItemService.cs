using AutoMapper;
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
        var newLibraryItem = _mapper.Map<DbLibraryItem>(request);

        await _libraryItemRepository.Create(newLibraryItem);

        //TODO: vill vi retunera något här?

    }
    public async Task UpdateLibraryItemAsync(UpdateLibraryItemRequest request)
    {
        if (await _libraryItemRepository.Exists(request.Title))
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

                var libraryItem = await _libraryItemRepository.Get(request.Title);

                await _libraryItemRepository.
                return true;
            }
            catch (Exception)
            {

                throw new BadHttpRequestException("LibraryItem not updated");
            }
        }
    }

}
