using ConsidTechnicalBackend.Services;
using ConsidTechnicalBackend.Models;
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
    public async Task<ActionResult> CreateLibraryItem([FromBody] CreateLibraryItemRequest request)
    {
        await _libraryItemService.CreateLibraryItemAsync(request);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateLibraryItem([FromBody] UpdateLibraryItemRequest UpdateLibraryItemRequest)
    {

        await _libraryItemService.UpdateLibraryItemAsync(UpdateLibraryItemRequest);
        return Ok();

    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteLibraryItem([FromQuery] int id)
    {

        await _libraryItemService.DeleteLibraryItemAsync(id);
        return Ok();

    }
    [HttpGet("get-by-categories")]
    public async Task<ActionResult> GetLibraryItemsByCategories()
    {

        var response = await _libraryItemService.GetLibraryItemsByCategoriesAsync();
        return Ok(response);

    }

    [HttpGet("get-by-types")]
    public async Task<ActionResult> GetLibraryItemsByTypes()
    {

        var response = await _libraryItemService.GetLibraryItemsByTypesAsync();
        return Ok(response);

    }
    // Following KIS(Keep it simple), could have added two specific enpoints one for check in and one for check out, but i include support for this in the UpdateLibraryItem,
    // I would also build fronend to take care of what types to be sent in, i could have used enums for this. Same with check in and out to be two diffrent buttons that generate diffrent fields
}