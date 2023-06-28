using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IActorsOfAward;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorOfAwardsController : ControllerBase
    {
        private readonly IActorsOfAward _actorsOfAwardServices;
        public ActorOfAwardsController(IActorsOfAward actorsOfAwardServices)
        {
            _actorsOfAwardServices = actorsOfAwardServices;
        }

        [HttpGet("getAllActorsOfAward")]
        public async Task<IActionResult> GetAllActorsOfAward()
        {
            try
            {
                return Ok(await _actorsOfAwardServices.GetAllActorsOfAwardAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addACtorsOfAward")]
        public async Task<IActionResult> AddActorsAward(ActorsOfAwardModelViews model)
        {
            try
            {
                return Ok(await _actorsOfAwardServices.AddActorsOfAwardAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editActorsAward")]
        public async Task<IActionResult> EditActorsAward(ActorsOfAwardModelViews model, int idPerson)
        {
            try
            {
                return Ok(await _actorsOfAwardServices.EditActorsOfAwardAsync(model, idPerson));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteActorsAward")]
        public async Task<IActionResult> DeleteActorsAward(int idPerson)
        {
            try
            {
                var getAll = await _actorsOfAwardServices.GetAllActorsOfAwardAsync();
                await _actorsOfAwardServices.DeleteActorsOfAwardAsync(idPerson);
                return Ok("Delete Sucess" + getAll);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
