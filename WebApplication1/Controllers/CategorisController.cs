using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.Services.ICategories;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorisController : ControllerBase
    {
        private readonly ICategories _categoriesServies;
        public CategorisController(ICategories categoriesServies)
        {
            _categoriesServies = categoriesServies;
        }
        [HttpGet("getAlllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _categoriesServies.GetCategoriesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
