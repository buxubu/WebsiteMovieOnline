using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IAward;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAward _awardServices;
        public AwardsController(IAward awardServices)
        {
            _awardServices = awardServices;
        }

        [HttpGet("getAllAward")]
        public async Task<IActionResult> GetAllAward()
        {
            try
            {
                return Ok(await _awardServices.GetAllAwardAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addAward")]
        public async Task<IActionResult> AddAward(AwardModelViews model)
        {
            try
            {
                return Ok(await _awardServices.AddAwardAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editAward")]
        public async Task<IActionResult> EditAward(AwardModelViews model, int idAward)
        {
            try
            {
                return Ok(await _awardServices.EditAwardAsync(model, idAward));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteAward")]
        public async Task<IActionResult> DeleteAward(int idAward)
        {
            try
            {
                await _awardServices.DeleteAwardAsync(idAward);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
