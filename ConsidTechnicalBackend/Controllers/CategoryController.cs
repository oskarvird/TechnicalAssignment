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
    //TODO: Id should be useed insted of title
    [HttpPost("create")]
    public async Task<ActionResult> CreateCategory([FromQuery] string categoryName) // using FromQuery because only one imparameter
    {
        if (await _categoryService.CreateCategoryAsync(categoryName))
        {
            return Ok();
        }
        else
        {

            return BadRequest("Category already exists");
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryRequest editCategoryRequest)
    {

        if (await _categoryService.UpdateCategoryAsync(editCategoryRequest))
        {
            return Ok();
        }
        else
        {
            return BadRequest("Category was not found");
        }
    }

    [HttpPut("delete")]
    public async Task<ActionResult> DeleteCategory([FromQuery] string categoryName)
    {
        if (await _categoryService.DeleteCategoryAsync(categoryName))
        {
            return Ok();
        }
        else
        {
            return BadRequest("Category was not found");
        }
    }
}