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

    }
}
