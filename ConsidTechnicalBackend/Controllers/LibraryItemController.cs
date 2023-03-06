using ConsidTechnicalBackend.Services;
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

        await _libraryItemService.UpdateLibraryItemAsync(UpdateLibraryItemRequest);
        return Ok();

    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteLibraryItem([FromQuery] string title)
    {

        if (await _libraryItemService.DeleteLibraryItemAsync(title))
        {
            return Ok();
        }
        else
        {
            return BadRequest("Library item not deleted");
        }
    }

    [HttpPut("check-in")]
    public async Task<ActionResult> CheckInLibraryItem([FromQuery] string title)
    {
        if (await _libraryItemService.CheckInLibraryItemAsync(title))
        {
            return Ok();
        }
        else
        {
            return BadRequest("Category was not found or couldnt be checked in");
        }
    }

    [HttpPut("check-out")]
    public async Task<ActionResult> CheckOutLibraryItem([FromBody] string title)
    {

        await _libraryItemService.CheckOutLibraryItemAsync(title);
        return Ok();

    }
}
