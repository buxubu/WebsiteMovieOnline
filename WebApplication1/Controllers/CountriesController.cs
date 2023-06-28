using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.ICountry;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountries _countriesServices;
        public CountriesController(ICountries countriesServices)
        {
            _countriesServices = countriesServices;
        }
        [HttpGet("getAllCountries")]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                return Ok(await _countriesServices.GetAllCountryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addCountries")]
        public async Task<IActionResult> AddCountries(CountriesModelViews model)
        {
            try
            {
                return Ok(await _countriesServices.AddCountryAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editCountries")]
        public async Task<IActionResult> EditCountries(CountriesModelViews model, int idCountries)
        {
            try
            {
                return Ok(await _countriesServices.EditCountryAsync(model, idCountries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpDelete("deleteCountries")]
        public async Task<IActionResult> DeleteCountries(int idCountries)
        {
            try
            {
                await _countriesServices.DeleteCountryAsync(idCountries);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
