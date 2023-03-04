using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidTechnicalBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryItemController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public LibraryItemController(
        ICategoryService categoryService
        )
    {
        _categoryService = categoryService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateLibraryItem([FromQuery] CreateLibraryItemRequest request)
    {

            return Ok();
    }
}
