using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMovieOnline.Models;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IProductionCompany;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionCompanyController : ControllerBase
    {
        private readonly IProductionCompanies _productionCompaniesServices;
        public ProductionCompanyController(IProductionCompanies productionCompaniesServices)
        {
            _productionCompaniesServices = productionCompaniesServices;
        }

        [HttpGet("getAllPC")]
        public async Task<IActionResult> GetAllPC()
        {
            try
            {
                return Ok(await _productionCompaniesServices.GetAllProductionCompaniesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addPC")]
        public async Task<IActionResult> AddPC(ProductionCompaniesModelViews model)
        {
            try
            {
                return Ok(await _productionCompaniesServices.AddProductionCompaniesAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editPC")]
        public async Task<IActionResult> EditPC(ProductionCompaniesModelViews model, int idPC)
        {
            try
            {
                return Ok(await _productionCompaniesServices.EditProductionCompaniesAsync(model, idPC));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deletePC")]
        public async Task<IActionResult> DeletePC(int idPC)
        {
            try
            {
                await _productionCompaniesServices.DeleteProductionCompaniesAsync(idPC);
                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
