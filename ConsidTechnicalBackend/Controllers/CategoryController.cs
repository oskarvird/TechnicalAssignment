using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsidTechnicalBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpPost("create")]
    public async Task CreateCategory([FromBody] string categoryName)
    {

    }
}
