using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidTechnicalBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryItemController : ControllerBase
{
    private readonly ILibraryItemService _libraryItemService;
    public LibraryItemController(
        ILibraryItemService libraryItemService
        )
    {
        _libraryItemService = libraryItemService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateLibraryItem([FromQuery] CreateLibraryItemRequest request)
    {
        await _libraryItemService.CreateLibraryItemAsync(request);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateLibraryItem([FromBody] UpdateLibraryItemRequest UpdateLibraryItemRequest)
    {


        return Ok();

    }
}
