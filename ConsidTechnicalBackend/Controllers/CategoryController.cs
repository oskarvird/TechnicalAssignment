using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsidTechnicalBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(
        ICategoryService categoryService
        )
    {
        _categoryService = categoryService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateCategory([FromQuery] string categoryName) // using FromQuery because only one imparameter
    {
        await _categoryService.CreateCategoryAsync(categoryName);

        return Ok();

    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryRequest editCategoryRequest)
    {

        await _categoryService.UpdateCategoryAsync(editCategoryRequest);
        return Ok();

    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteCategory([FromQuery] int id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return Ok();

    }
}