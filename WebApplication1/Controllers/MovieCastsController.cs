using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IMovieCasts;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCastsController : ControllerBase
    {
        private readonly IMovieCasts _movieCastServices;
        public MovieCastsController(IMovieCasts movieCastServices)
        {
            _movieCastServices = movieCastServices;
        }
        [HttpGet("getAllMovieCast")]
        public async Task<IActionResult> GetAllMovieCast()
        {
            try
            {
                return Ok(await _movieCastServices.GetAllMovieCastAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addMovieCast")]
        public async Task<IActionResult> AddMovieCast(MovieCastModelViews model)
        {
            try
            {
                return Ok(await _movieCastServices.AddMovieCastAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editMovieCast")]
        public async Task<IActionResult> EditMovieCast(MovieCastModelViews model, int idPerson)
        {
            try
            {
                return Ok(await _movieCastServices.EditMovieCastAsync(model, idPerson));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteMovieCast")]
        public async Task<IActionResult> DeleteMovieCast(int idPerson)
        {
            try
            {
                await _movieCastServices.DeleteMovieCastAsync(idPerson);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
