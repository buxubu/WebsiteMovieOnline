using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IDirectors;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectors _directorsServices;
        public DirectorsController(IDirectors directorsServices)
        {
            _directorsServices = directorsServices;
        }

        [HttpGet("getAllDirectors")]
        public async Task<IActionResult> GetAllDirectors()
        {
            try
            {
                return Ok(await _directorsServices.GetAllDirectorsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addDirectors")]
        public async Task<IActionResult> AddDirectors(DirectorsModelViews model)
        {
            try
            {
                return Ok(await _directorsServices.AddDirectorsAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editDirectors")]
        public async Task<IActionResult> EditDirectors(DirectorsModelViews model, int idDirectors)
        {
            try
            {
                return Ok(await _directorsServices.EditDirectorsAsync(model, idDirectors));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteDirectors")]
        public async Task<IActionResult> DeleteDirectors(int idDirectors)
        {
            try
            {
                await _directorsServices.DeleteDirectors(idDirectors);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
