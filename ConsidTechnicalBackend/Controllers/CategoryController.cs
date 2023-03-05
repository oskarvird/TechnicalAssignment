using Azure;
using ConsidTechnicalBackend.Models;
using ConsidTechnicalBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    public async Task<ActionResult> CreateCategory([FromQuery] string categoryName) // using FromQueary because only one imparameter
    {
        if (await _categoryService.CreateCategoryAsync(categoryName))
        {
            return BadRequest("Category already exists");
        }
        else
        {
            return Ok();
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryRequest editCategoryRequest)
    {

        if (await _categoryService.UpdateCategoryAsync(editCategoryRequest))
        {
            return BadRequest("Category was not found");
        }
        else
        {
            return Ok();
        }
    }

    [HttpPut("delete")]
    public async Task<ActionResult> DeleteCategory([FromQuery] string categoryName)
    {
        if (await _categoryService.DeleteCategoryAsync(categoryName))
        {
            return BadRequest("Category was not found");
        }
        else
        {
            return Ok();
        }
    }
}