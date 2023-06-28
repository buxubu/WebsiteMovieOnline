using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IGenres;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenres _genresServices;
        public GenresController(IGenres genresServices)
        {
            _genresServices = genresServices;
        }

        [HttpGet("getAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                return Ok(await _genresServices.GetAllGenresAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addGenres")]
        public async Task<IActionResult> AddGenres(GenresModelViews model)
        {
            try
            {
                if (model == null) return NotFound();
                return Ok(await _genresServices.AddGenresAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editGenres")]
        public async Task<IActionResult> EditGenres(GenresModelViews model, int idGenres)
        {
            try
            {
                return Ok(await _genresServices.EditGenresAsync(model, idGenres));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteGenres")]
        public async Task<IActionResult> DeleteGenres(int idGenres)
        {
            try
            {
                await _genresServices.DeleteGenresAsync(idGenres);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
