using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IActors;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActors _actorsServices;

        public ActorsController(IActors actorsServices)
        {
            _actorsServices = actorsServices;
        }

        [HttpGet("getAllActors")]
        public async Task<IActionResult> GetAllActors()
        {
            try
            {
                return Ok(await _actorsServices.GetAllActorsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addActors")]
        public async Task<IActionResult> AddActors(ActorsModelViews model)
        {
            try
            {
                if (model == null) return NotFound();
                return Ok(await _actorsServices.AddActorsAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editActors")]
        public async Task<IActionResult> EditActors(ActorsModelViews model, int idActors)
        {
            try
            {
                if (model == null) return NotFound();
                return Ok(await _actorsServices.EditActorsAsync(model, idActors));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteActors")]
        public async Task<IActionResult> DeleteActors(int idActos)
        {
            try
            {
                var result = await _actorsServices.GetAllActorsAsync();
                await _actorsServices.DeleteActorAsync(idActos);
                return Ok("Delete Success " + result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
